using Newtonsoft.Json;

namespace NagerResponse.DataEntities
{
    public class DateJson
    {
        [JsonProperty(PropertyName = "date")] public string Date { get; set; }
        [JsonProperty(PropertyName = "localName")] public string LocalName { get; set; }
        [JsonProperty(PropertyName = "name")] public string Name { get; set; }
        [JsonProperty(PropertyName = "countryCode")] public string CountryCode { get; set; }
        [JsonIgnore]
        [JsonProperty("type")] public string Type { get; set; }
    }
}
