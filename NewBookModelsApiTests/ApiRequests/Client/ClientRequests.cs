using System.Collections.Generic;
using NewBookModelsApiTests.Models.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace NewBookModelsApiTests.ApiRequests.Client
{
    public class ClientRequests
    {
        public static string SendRequestChangeClientEmailPost(string password, string email, string token)
        {
            var client = new RestClient("https://api.newbookmodels.com/api/v1/client/change_email/");
            var request = new RestRequest(Method.POST);
            
            var newEmailModel = new Dictionary<string, string>
            {
                {"email", email},
                {"password", password}
            };

            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", token);
            request.AddJsonBody(newEmailModel);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var changeEmailResponse = JsonConvert.DeserializeObject<ChangeEmailResponse>(response.Content);

            return changeEmailResponse.Email;
        }

        public static string SendRequestChangeClientPhonePost(string password, string phoneNumber, string token)
        {
            var client = new RestClient("https://api.newbookmodels.com/api/v1/client/change_phone/");
            var request = new RestRequest(Method.PUT);
            
            var newPhoneModel = new Dictionary<string, string>
            {
                {"phone_number", phoneNumber},
                {"password", password}
            };
            
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", token);
            
            request.AddJsonBody(newPhoneModel);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var changePhoneResponse = JsonConvert.DeserializeObject<ChangePhoneResponse>(response.Content);

            return changePhoneResponse.PhoneNumber;
        }
    }
}