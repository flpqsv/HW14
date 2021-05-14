using Newtonsoft.Json;

namespace NewBookModelsApiTests.Models.Client
{
    public class ChangePhoneResponse
    {
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}