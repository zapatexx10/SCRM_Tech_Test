using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Application.Features.Promotions.GetAll.V2;
using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Models;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.GetAll.V2;

public class PromotionsControllerTests
{
    private readonly Mock<ILogger<PromotionsController>> _loggerMock;
    private readonly Mock<IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse>> _handlerMock;
    private readonly PromotionsController _controller;

    public PromotionsControllerTests()
    {
        _loggerMock = new Mock<ILogger<PromotionsController>>();
        _handlerMock = new Mock<IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse>>();

        //We need to add this in order to make the Request.Path of the ProblemDetails,
        //if not it will be null and the assertion will fail with null reference exception
        var context = new DefaultHttpContext();

        _controller = new PromotionsController(_handlerMock.Object, _loggerMock.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = context
            }
        };
    }

    [Fact]
    public async Task GivenValidCountryCodeAndLang_WhenGettingPromotions_ThenReturnsValidPromotions()
    {
        //Arrange
        var promotionResults = new List<PromotionModel>() { CreatePromotionModel() };
        var countryCode = "ES";
        var lang = "EN";
        var maxPromotions = 2;
        var totalCount = promotionResults.Count;
        var request = new GetAllPromotionsRequest(countryCode, lang, maxPromotions);
        var response = new GetAllPromotionsResponse().SetPromotions(promotionResults, totalCount);

        _handlerMock.Setup(r => r.HandleAsync(request, default))
            .ReturnsAsync(response);
        
        //Act
        var result = await _controller.Get(countryCode, lang, maxPromotions, default);

        //Assert
        _handlerMock.Verify(r => r.HandleAsync(request, default), Times.Once);
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsType<GetAllPromotionsResponse>(okResult.Value);
        Assert.Equal(totalCount, (okResult.Value as GetAllPromotionsResponse)?.TotalCount);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task GivenNoPromotionsFound_WhenGettingPromotions_ThenReturnsInternalServerError()
    {
        //Arrange
        var lang = "EN";
        var countryCode = "ES";
        var maxPromotions = 2;
        var request = new GetAllPromotionsRequest(countryCode, lang, maxPromotions);
        var response = new GetAllPromotionsResponse().SetException(new Exception("No promotions found"));
        
        _handlerMock.Setup(r => r.HandleAsync(request, default))
            .ReturnsAsync(response);

        //Act
        var result = await _controller.Get(countryCode, lang, maxPromotions, default);

        //Assert
        _handlerMock.Verify(r => r.HandleAsync(request, default), Times.Once);
        Assert.IsType<ObjectResult>(result);
        var okResult = result as ObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(500, okResult.StatusCode);
    }

    private static PromotionModel CreatePromotionModel()
    {
        return new PromotionModel
        {
            PromotionId = Guid.NewGuid(),
            EndValidityDate = DateTime.UtcNow.AddDays(7),
            Texts = new PromotionTextsModel
            {
                Title = "Test Title",
                Description = "Test Description",
                DiscountTitle = "Discount",
                DiscountDescription = "Discount Description"
            },
            Images = new List<string> { "https://example.com/image1.png" },
            Discounts = new List<Discount>
            {
                new OnlineDiscount
                {
                    OriginalPrice = 50,
                    FinalPrice = 35,
                    LowestPriceLast30Days = 35,
                    PriceType = "EUR",
                    UnitsToBuy = 1,
                    UnitsToPay = 1,
                    HasPrice = true
                }
            }
        };
    }
}
