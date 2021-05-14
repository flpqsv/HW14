using Newtonsoft.Json;

namespace NewBookModelsApiTests.Models.Client
{
    public class ChangeIndustryAndLocationResponse
    {
        [JsonProperty("industry")]
        public string Industry { get; set; }
        [JsonProperty("location_admin1_code")]
        public string LocationCode { get; set; }
        [JsonProperty("location_city_name")]
        public string LocationCity { get; set; }
        [JsonProperty("location_latitude")]
        public string LocationLatitude { get; set; }
        [JsonProperty("location_longitude")]
        public string LocationLongitude { get; set; }
        [JsonProperty("location_name")]
        public string LocationName { get; set; }
        [JsonProperty("location_timezone")]
        public string LocationTime { get; set; }
    }
}