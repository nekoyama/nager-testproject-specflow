using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NagerResponse.DataEntities
{
    //Response Object
    public class NagerResponseJson
    {
        [JsonProperty(PropertyName = "date")] public string Date { get; set; }
        [JsonProperty(PropertyName = "localName")] public string LocalName { get; set; }
        [JsonProperty(PropertyName = "name")] public string Name { get; set; }
        [JsonProperty(PropertyName = "countryCode")] public string CountryCode { get; set; }
        [JsonIgnore]
        [JsonProperty(PropertyName = "_fixed")] public bool Fixed { get; set; }
        [JsonIgnore]
        [JsonProperty(PropertyName = "global")] public string Global { get; set; }
        [JsonIgnore]
        [JsonProperty(PropertyName = "counties")] public object Counties { get; set; }
        [JsonIgnore]
        [JsonProperty(PropertyName = "launchYear")] public int LaunchYear { get; set; }
        [JsonProperty(PropertyName = "type")] public string Type { get; set; }

        [JsonProperty("dates")]
        public List<DateJson> Dates { get; set; } //List of Dates in the response Object

    }
}


