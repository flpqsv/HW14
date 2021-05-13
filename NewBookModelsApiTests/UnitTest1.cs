using System;
using System.Collections.Generic;
using NewBookModelsApiTests.Models.Auth;
using NewBookModelsApiTests.Models.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace NewBookModelsApiTests
{
    public class UnitTest1
    {
        [Test]
        public void Test1()
        {
            //var user = CreateUserViaApi();

            var changedEmail = SendRequesrChangeClinetEmailPost();
        }
        
        public ClientAuthModel CreateUserViaApi()
        {
            var client = new RestClient("https://api.newbookmodels.com/api/v1/auth/client/signup/");
            var request = new RestRequest(Method.POST);

            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", "Mabel123!"},
                {"phone_number", "3453453454"}
            };

            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(user);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var createdUser = JsonConvert.DeserializeObject<ClientAuthModel>(response.Content);

            return createdUser;
        }

        public string SendRequesrChangeClinetEmailPost()
        {
            var user = CreateUserViaApi();
            
            var client = new RestClient("https://api.newbookmodels.com/api/v1/client/change_email/");
            var request = new RestRequest(Method.POST);

            var newEmailModel = new Dictionary<string, string>
            {
                {"email", $"mabel1{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"password", "Mabel123!"}
            };

            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", user.TokenData.ToString());
            request.AddJsonBody(newEmailModel);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var newEmail = JsonConvert.DeserializeObject<ChangeEmailResponse>(response.Content);

            return newEmail.ToString();
        }
    }
}