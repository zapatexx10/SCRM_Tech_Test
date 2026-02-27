using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace PromotionEngine.Application.GetAll.V2;

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
        var url = "/v2/ES/promotions?languageCode=ES&maxPromotions=2";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GivenInvalidLanguageCode_WhenGettingPromotions_ThenReturnsBadRequest()
    {
        // Arrange: 
        var url = "/v2/ES/promotions?languageCode=E&maxPromotions=2";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenInvalidCountryCode_WhenGettingPromotions_ThenReturnsBadRequest()
    {
        // Arrange: 
        var url = "/v2/E/promotions?languageCode=ES&maxPromotions=2";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenInvalidMaxPromotions_WhenGettingPromotions_ThenReturnsBadRequest()
    {
        var maxNumber = long.MaxValue;
        // Arrange: 
        var url = $"/v2/ES/promotions?languageCode=ES&maxPromotions={maxNumber}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
