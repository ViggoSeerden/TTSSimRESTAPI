using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TTSSim.xUnit
{
    public class UnitTest1 :
    IClassFixture<MockFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly MockFactory<Program>
            _factory;

        public UnitTest1(
            MockFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Test1()
        {
            var response = await _client.GetAsync("/api/User/GetAll");

            response.EnsureSuccessStatusCode();
        }
    }
}