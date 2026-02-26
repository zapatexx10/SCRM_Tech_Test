using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Application.Features.Promotions.GetById.V1;
using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Models;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.GetById.V1;

public class GetAllControllerTests
{
    private readonly Mock<ILogger<PromotionsController>> _loggerMock;
    private readonly Mock<IHandler<GetPromotionByIdRequest, GetPromotionByIdResponse>> _handlerMock;
    private readonly PromotionsController _controller;

    public GetAllControllerTests()
    {
        _loggerMock = new Mock<ILogger<PromotionsController>>();
        _handlerMock = new Mock<IHandler<GetPromotionByIdRequest, GetPromotionByIdResponse>>();

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
        var promotionResult = CreatePromotionModel();
        var countryCode = "ES";
        var lang = "EN";
        var id = promotionResult.PromotionId;
        var request = new GetPromotionByIdRequest(countryCode, lang, id);
        var response = new GetPromotionByIdResponse().SetPromotion(promotionResult);

        _handlerMock.Setup(r => r.HandleAsync(request, default))
            .ReturnsAsync(response);

        //Act
        var result = await _controller.GetById(countryCode, lang, id, default);

        //Assert
        _handlerMock.Verify(r => r.HandleAsync(request, default), Times.Once);
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(response, okResult.Value);
        Assert.IsType<GetPromotionByIdResponse>(okResult.Value);
        var okResponse = okResult.Value as GetPromotionByIdResponse;
        Assert.NotNull(okResponse);
        Assert.NotNull(okResponse.Promotion);
        Assert.Equal(id, okResponse.Promotion.PromotionId);
    }

    [Fact]
    public async Task GivenNoPromotionsFound_WhenGettingPromotions_ThenReturnsInternalServerError()
    {
        //Arrange
        var lang = "EN";
        var countryCode = "ES";
        var id = Guid.NewGuid();
        var request = new GetPromotionByIdRequest(countryCode, lang, id);
        var response = new GetPromotionByIdResponse().SetException(new Exception("No promotions found"));

        _handlerMock.Setup(r => r.HandleAsync(request, default))
            .ReturnsAsync(response);

        //Act
        var result = await _controller.GetById(countryCode, lang, id, default);

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
