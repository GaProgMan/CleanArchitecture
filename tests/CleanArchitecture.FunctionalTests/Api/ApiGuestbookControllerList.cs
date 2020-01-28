using System.Net.Http;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
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
            var result = JsonConvert.DeserializeObject<Guestbook>(stringResponse);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            //Assert.NotEmpty(result.Entries);
            Assert.Single(result.Entries);
        }
    }
}