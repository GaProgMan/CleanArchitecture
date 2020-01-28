using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace CleanArchitecture.FunctionalTests.Api
{
    public class ApiGuestbookControllerNewEntry : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        
        public ApiGuestbookControllerNewEntry(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenInvalidIdReturnsNotFound()
        {
            var invalidId = -1;
            var entityToPost = new { EmailAddress = "test@test.test", Message = "a new message here" };
            var jsonEntity = new StringContent(JsonConvert.SerializeObject(entityToPost), Encoding.UTF8, "application/json"); //hardcoded string?

            var response = await _client.PostAsync($"/api/guestbook/{invalidId}/newentry", jsonEntity);
            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }
        
        [Fact]
        public async Task GivenValidIdReturnOK()
        {
            var validId = 1;
            var entityToPost = new { EmailAddress = "test@test.test", Message = "a new message here" };
            var jsonEntity = new StringContent(JsonConvert.SerializeObject(entityToPost), Encoding.UTF8, "application/json"); //hardcoded string?

            var response = await _client.PostAsync($"/api/guestbook/{validId}/newentry", jsonEntity);
            response.EnsureSuccessStatusCode();
            
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            Assert.NotNull(stringResponse);
            var result = JsonConvert.DeserializeObject<Guestbook>(stringResponse);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            //Assert.NotEmpty(result.Entries);
            Assert.NotEqual(1, result.Entries.Count);
        }
    }
}