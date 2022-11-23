using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TTSSimRESTAPI.Models;

namespace TTSSim.Test
{
    [TestClass]
    public class IntegrationUserTests
    {
        private HttpClient client;
    
        public IntegrationUserTests()
        {
            var factory = new WebApplicationFactory<Program>();
            client = factory.CreateClient();
        }

        [TestMethod]
        public async Task Test_GetoggiVAdmin()
        {
            var response = await client.GetAsync("/api/User/Get?id=2");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("oggiVAdmin"));
        }

        [TestMethod]
        public async Task Test_GetAllUsers()
        {
            var response = await client.GetAsync("/api/User/GetAll");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("oggiVAdmin"));
            //Assert.IsTrue(responsestring.Contains("oggiVUser"));
        }

        [TestMethod]
        public async Task Test_GetNonExistingUser()
        {
            var response = await client.GetAsync("/api/User/Get?id=-1");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("404"));
        }
        
        [TestMethod]
        public async Task Test_AddUser()
        {
            User user = new User()
            { 
                Username = "thisismorethan20characters",
                UserType = 0,
                Token = "0"
            };

            var content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user)));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("/api/User/Add", content);
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("thisismorethan20characters"));
        }

        [TestMethod]
        public async Task Test_DeleteUser()
        {
            var response = await client.DeleteAsync("/api/User/DeleteByName?name=thisismorethan20characters");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("200"));
        }
        
        [TestMethod]
        public async Task Test_DeleteNonExistingUser()
        {
            var response = await client.DeleteAsync("/api/User/Delete?id=-1");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("404"));
        }


        //[TestMethod]
        //public async Task TestMethod2()
        //{
        //    var webappfactory = new MockFactory();
        //    HttpClient httpclient = webappfactory.CreateClient();

        //    var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/User/Add");

        //    User user = new User()
        //    {
        //        Username = "thisismorethan20characters",
        //        UserType = 0,
        //        Token = "0"
        //    };

        //    var content = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user)));
        //    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    var response = await httpclient.PostAsync("/api/User/Add", content);
        //    var responsestring = await response.Content.ReadAsStringAsync();

        //    response.EnsureSuccessStatusCode();
        //    Assert.IsTrue(responsestring.Contains("thisismorethan20characters"));
        //}
    }
}