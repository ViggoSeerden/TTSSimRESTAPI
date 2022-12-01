using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TTSSimIntegrationTests;
using TTSSimRESTAPI.Data;
using TTSSimRESTAPI.Models;

namespace TTSSim.Test
{
    [TestClass]
    public class IntegrationUserTests
    {
        private HttpClient client;

        public IntegrationUserTests()
        {
            //var factory = new WebApplicationFactory<Program>();
            var factory = new MockFactory();
            using (var scope = factory.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var APIContext = provider.GetRequiredService<APIContext>())
                {
                    APIContext.Database.EnsureCreatedAsync();

                    APIContext.Users.AddAsync(new User { Username = "oggiVAdmin", Id = 1 });
                    APIContext.Users.AddAsync(new User { Username = "oggiVUser", Id = 2 });
                    APIContext.SaveChangesAsync();
                }
                client = factory.CreateClient();
            }
        }

        [TestMethod]
        public async Task Test5_GetoggiVAdmin()
        {
            var response = await client.GetAsync("/api/User/Get?id=1");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("oggiVAdmin"));
        }

        [TestMethod]
        public async Task Test2_GetAllUsers()
        {
            var response = await client.GetAsync("/api/User/GetAll");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("oggiVAdmin"));
            Assert.IsTrue(responsestring.Contains("oggiVUser"));
        }

        [TestMethod]
        public async Task Test3_GetNonExistingUser()
        {
            var response = await client.GetAsync("/api/User/Get?id=-1");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("404"));
        }

        public static int TestUserId;

        [TestMethod]
        public async Task Test1_AddUser()
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
            string responsestring = await response.Content.ReadAsStringAsync();

            TestUserId = Convert.ToInt32(responsestring.Substring(15, 15).Substring(0, 1));

            response.EnsureSuccessStatusCode();
            Assert.IsTrue(responsestring.Contains("thisismorethan20characters"));
        }

        [TestMethod]
        public async Task Test4_DeleteUser()
        {
            var response = await client.DeleteAsync("/api/User/Delete?id=2");
            var responsestring = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task Test6_DeleteNonExistingUser()
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