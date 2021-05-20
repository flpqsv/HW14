using System;
using System.Collections.Generic;
using System.Net;
using System.Web.UI.WebControls;
using NewBookModelsApiTests.ApiRequests.Auth;
using NewBookModelsApiTests.ApiRequests.Client;
using NewBookModelsApiTests.Models.Auth;
using NewBookModelsApiTests.Models.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace NewBookModelsApiTests
{
    public class AccountSettingsTests
    {
        [Test]
        public void ChangeEmail()
        {
            var password = "Mabel123!";

            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", password},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);

            var newEmail = $"mabel{DateTime.Now:ddyymmHHssmm}@gmail.com";

            var responseModel =
                ClientRequests.SendRequestChangeClientEmailPost(password, newEmail, createdUser.TokenData.Token);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(newEmail, responseModel.Model.Email);
                Assert.AreEqual(HttpStatusCode.OK, responseModel.Response.StatusCode);
            });
        }
        
        [Test]
        public void ChangePassword()
        {
            var password = "Mabel123!";
            var email = $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com";

            var user = new Dictionary<string, string>
            {
                {"email", email},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", password},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);

            var newPassword = "MaBelle1234!";

            var changedPassword = ClientRequests.SendRequestChangeClientPasswordPost(password, newPassword, createdUser.TokenData.Token);

             var statusCode = SignIn(email, newPassword);
            
           Assert.AreEqual(200, statusCode);
        }

        public int SignIn(string email, string newPassword)
        {
            var client = new RestClient("https://api.newbookmodels.com/api/v1/auth/signin/");
            var request = new RestRequest(Method.POST);
            
            var signInModel = new Dictionary<string, string>
            {
                {"password", newPassword},
                {"email", email}
            };
            
            request.AddHeader("content-type", "application/json");
            
            request.AddJsonBody(signInModel);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            int statusCode = (int) response.StatusCode;

            return statusCode;
        }
        
        
        [Test]
        public void ChangePhone()
        {
            var password = "Mabel123!";

            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", password},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);

            var newPhone = "1453453454";

            var changedPhone = ClientRequests.SendRequestChangeClientPhonePost(password, newPhone, createdUser.TokenData.Token);

            Assert.AreEqual(newPhone, changedPhone);
        }

        [Test]
        public void ChangePersonalInfo()
        {
            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", "Mabel123!"},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);

            var firstName = "Svitlana";
            var lastName = "Filippovych";

            var responseModel =
                ClientRequests.SendRequestChangeClientPersonalInfoPost(firstName, lastName,
                    createdUser.TokenData.Token);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstName, responseModel.Model.FirstName);
                Assert.AreEqual(lastName, responseModel.Model.LastName);
                Assert.AreEqual(HttpStatusCode.OK, responseModel.Response.StatusCode);
            });
        }

        [Test]
        public void ChangeIndustryAndLocation()
        {
            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", "Mabel123!"},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);
            
            var industry = "Fashion"; 
            var locationCode = "TX";
            var locationCity = "Texas";
            var locationLatitude = "32.7078751";
            var locationLongitude = "-96.9209135";
            var locationName = "Dallas-Fort Worth Metropolitan Area, TX, USA";
            var locationTime = "America/Chicago";
            
            var responseModel =
                ClientRequests.SendRequestChangeClientIndustryAndLocationPost(industry, locationCode, locationCity, 
                    locationLatitude, locationLongitude, locationName, locationTime, createdUser.TokenData.Token);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(industry, responseModel.Model.Industry);
                Assert.AreEqual(locationCode, responseModel.Model.LocationCode);
                Assert.AreEqual(locationCity, responseModel.Model.LocationCity);
                Assert.AreEqual(locationLatitude, responseModel.Model.LocationLatitude);
                Assert.AreEqual(locationLongitude, responseModel.Model.LocationLongitude);
                Assert.AreEqual(locationName, responseModel.Model.LocationName);
                Assert.AreEqual(locationTime, responseModel.Model.LocationTime);
                Assert.AreEqual(HttpStatusCode.OK, responseModel.Response.StatusCode);
            });
        }
        
        [Test]
        public void ChangeCompanyInfo()
        {
            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", "Mabel123!"},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);
            
            var description = "My description."; 
            var companyName = "Henlo World Inc";
            var website = "http://henloworld.com";
            
            var responseModel = ClientRequests.SendRequestChangeClientCompanyInfoPost(description, companyName, website, createdUser.TokenData.Token);
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(description, responseModel.Model.Description);
                Assert.AreEqual(companyName, responseModel.Model.CompanyName);
                Assert.AreEqual(website, responseModel.Model.Website);
                Assert.AreEqual(HttpStatusCode.OK, responseModel.Response.StatusCode);
            });
        }
        
        [Test]
        public void TryChangeEmailWithWrongPassword()
        {
            var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", "Mabel123!"},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);

            var newEmail = $"new.mabel{DateTime.Now:ddyymmHHssmm}@gmail.com";
            var wrongPassword = "randomPass";

            var responseModel =
                ClientRequests.SendRequestChangeClientEmailPost(wrongPassword, newEmail, createdUser.TokenData.Token);

            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(newEmail, responseModel.Model.Email);
                Assert.AreNotEqual(HttpStatusCode.OK, responseModel.Response.StatusCode);
            });
        }

        [Test]
        public void TryToChangePasswordWithWrongCurrentPassword()
        {var user = new Dictionary<string, string>
            {
                {"email", $"mabel{DateTime.Now:ddyymmHHssmmffff}@gmail.com"},
                {"first_name", "MaBelle"},
                {"last_name", "Parker"},
                {"password", "Mabel123!"},
                {"phone_number", "3453453454"}
            };

            var createdUser = AuthRequests.SendRequestClientSignUpPost(user);

            var newPhone = "1453453454";
            var wrongPassword = "randomPass";

            var changedPhone = ClientRequests.SendRequestChangeClientPhonePost(wrongPassword, newPhone, createdUser.TokenData.Token);

            Assert.AreNotEqual(newPhone, changedPhone);
        }
        
        [Test]
        public void Upload()
        {
            var client = new RestClient("https://api.newbookmodels.com/api/images/upload/%22");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Disposition", "form-data; name='file'; filename='67913270_2607448572640247_7383304177759289344_o.jpeg'");
            request.AddHeader("content-type", "image/jpeg");
            request.AddHeader("authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2MjE1MzI0NzgsInVzZXJfaWQiOiIxMzhiODIwNy01YWM4LTQ5ZjAtYTcyNi0xMzllMDQ2ZWFlNjUiLCJleHAiOjE2MjQxMjQ0Nzh9.3OkE4lM-0MozMZQ39GtQlV422PUiby1feIzAJrQabY0");
            request.AddParameter("image/jpeg", "/Users/MaBelle/Downloads/67913270_2607448572640247_7383304177759289344_o.jpeg", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            
            var client1 = new RestClient("https://api.newbookmodels.com/api/v1/client/self/%22");
            client1.Timeout = -1;
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2MjE1MzI0NzgsInVzZXJfaWQiOiIxMzhiODIwNy01YWM4LTQ5ZjAtYTcyNi0xMzllMDQ2ZWFlNjUiLCJleHAiOjE2MjQxMjQ0Nzh9.3OkE4lM-0MozMZQ39GtQlV422PUiby1feIzAJrQabY0");
            request1.AlwaysMultipartFormData = true;
            request1.AddParameter("avatar_id", "334b960e-4d92-4bc5-a059-4540ca2fa8af");
            IRestResponse response1 = client.Execute(request1);
            
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreNotEqual(HttpStatusCode.OK, response.StatusCode);
            });
        }
    }
}