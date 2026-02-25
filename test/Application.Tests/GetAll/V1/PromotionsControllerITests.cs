using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace PromotionEngine.Application.GetAll.V1
{
    public class PromotionsControllerITests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PromotionsControllerITests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenValidParameters_WhenGettingPromotions_ThenReturnsOk()
        {
            // Arrange
            var url = "/v1/ES/promotions?languageCode=ES";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenMissingLanguageCode_WhenGettingPromotions_ThenReturnsBadRequest()
        {
            // Arrange: 
            var url = "/v1/ES/promotions?languageCode=";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenInvalidCountryCode_WhenGettingPromotions_ThenReturnsBadRequest()
        {
            // Arrange: 
            var url = "/v1/E/promotions?languageCode=ES";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
