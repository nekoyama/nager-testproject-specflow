using System;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using GreenfluxHilda.DataEntities;
using Assert = NUnit.Framework.Assert;
using System.Collections.Generic;
using System.Globalization;

namespace NagerDateApi
{
    public class NagerRequestData
    {
        public string CurrentYear = DateTime.Now.Year.ToString();
        public string RequestYear;
        public string CountryCode;
        public string HolidayName;
        public string HolidayDate;
        public int StatusCode;
    }

    [Binding]
    public class Nager_DateSteps
    {
        RestClient client;
        RestRequest request;
        IRestResponse response;
        //IEnumerable<DateJson> 

        private readonly NagerRequestData requestData;
        public Nager_DateSteps(NagerRequestData requestData)
        {
            this.requestData = requestData;
        }


        [Given(@"the Country Code '(.*)' and the Holiday '(.*)'")]
        public void GivenRequestData(string countryCode, string expectedHolidayName)
        {
            this.requestData.CountryCode = countryCode;
            this.requestData.HolidayName = expectedHolidayName;
            this.requestData.StatusCode = 200;
        } 

        [When(@"I send the request")]
        public void WhenISendTheRequest()
        {
            //var client = new ThrottledRestClient(60);
            RestClient restClient = new RestClient("https://date.nager.at/api/v2/PublicHolidays/");
            client = restClient;
            request = new RestRequest($"{this.requestData.CurrentYear}/{this.requestData.CountryCode}", Method.GET);
            response = client.Execute(request);
           
        }


        [Then(@"the expected date is correct for '(.*)' years in the past and for '(.*)' years in the future")]
        public void ThenTheExpectedDateIsCorrect(int NumberOfFutureYears, int numberOfPreviousYears)
        {

            int nowYear = int.Parse(this.requestData.CurrentYear);
            int futureYear = nowYear + NumberOfFutureYears;
            int pastYear = nowYear - numberOfPreviousYears;

            for (int i = nowYear; i < futureYear; i++)
            {
                request = new RestRequest($"{i}/{this.requestData.CountryCode}", Method.GET);
                response = client.Execute(request);
                Assert.That((int)response.StatusCode, Is.EqualTo(this.requestData.StatusCode), $"The status code was #{response.StatusCode}");
                var nagerResponseJson = JsonConvert.DeserializeObject<List<NagerResponseJson>>(response.Content);
                Assert.That(nagerResponseJson[0].Name, Is.EqualTo(this.requestData.HolidayName));
            }

            for (int i = pastYear; i < nowYear; i++)
            {
                request = new RestRequest($"{i}/{this.requestData.CountryCode}", Method.GET);
                response = client.Execute(request);
                Assert.That((int)response.StatusCode, Is.EqualTo(this.requestData.StatusCode), $"The status code was #{response.StatusCode}");
                var nagerResponseJson = JsonConvert.DeserializeObject<List<NagerResponseJson>>(response.Content);
                Assert.That(nagerResponseJson[0].Name.Contains(this.requestData.HolidayName), $"The response contained #{nagerResponseJson[0].Name}");
            }
        }

        [Then(@"the WeekDay should be '(.*)'")]
        public void ThenTheWeekDayShouldBe(string weekDay)
        {
            Assert.That((int)response.StatusCode, Is.EqualTo(this.requestData.StatusCode), $"The status code was #{response.StatusCode}");
            var nagerResponseJson = JsonConvert.DeserializeObject<List<NagerResponseJson>>(response.Content);

            System.Collections.IList list = nagerResponseJson;

            for (int i = 0; i < list.Count; i++)
            {
                if(nagerResponseJson[i].Name.Contains(this.requestData.HolidayName))
                {
                    this.requestData.HolidayDate = nagerResponseJson[i].Date;
                    DateTime dateTime = DateTime.Parse(this.requestData.HolidayDate);
                    string weekDayResult = (dateTime.ToString("dddd",
                                new CultureInfo("us-US")));
                    Assert.That((string)weekDayResult, Is.EqualTo(weekDay), $"The Week Day is not #{weekDay}");
                    return; 
                }
            }
            Assert.Fail();
        }
    }
}
