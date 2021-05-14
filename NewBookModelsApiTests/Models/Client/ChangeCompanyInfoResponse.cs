using Newtonsoft.Json;

namespace NewBookModelsApiTests.Models.Client
{
    public class ChangeCompanyInfoResponse
    {
        [JsonProperty("company_description")]
        public string Description { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("company_website")]
        public string Website { get; set; }
    }
}