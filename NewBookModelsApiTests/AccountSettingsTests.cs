using System;
using System.Collections.Generic;
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

            var changedEmail =
                ClientRequests.SendRequestChangeClientEmailPost(password, newEmail, createdUser.TokenData.Token);

            Assert.AreEqual(newEmail, changedEmail);
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
    }
}