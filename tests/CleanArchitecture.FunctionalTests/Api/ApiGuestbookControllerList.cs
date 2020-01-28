using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Web;
using CleanArchitecture.Web.ApiModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class ApiGuestbookControllerList : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        
        public ApiGuestbookControllerList(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task GivenNoIdReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/guestbook");
            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }
        
        [Fact]
        public async Task GivenInvalidIdReturnsNotFound()
        {
            var invalidId = -1;
            var response = await _client.GetAsync($"/api/guestbook{invalidId}");
            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }

        [Fact]
        public async Task ReturnsGuestBookAndEntry()
        {
            var response = await _client.GetAsync("/api/guestbook/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            Assert.NotNull(stringResponse);
            var result = JsonConvert.DeserializeObject<GuestbookDTO>(stringResponse);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            //Assert.NotEmpty(result.Entries);
            Assert.Single(result.Entries);
        }
    }
}