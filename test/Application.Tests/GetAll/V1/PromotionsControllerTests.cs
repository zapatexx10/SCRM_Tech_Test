using PromotionEngine.Application.Features.Promotions.GetAll.V1;
using PromotionEngine.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Application.GetAll.V1;

public class PromotionsControllerTests
{
    private readonly Mock<ILogger<PromotionsController>> _loggerMock;
    private readonly Mock<IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse>> _handlerMock;
    private readonly PromotionsController _controller;

    public PromotionsControllerTests()
    {
        _loggerMock = new Mock<ILogger<PromotionsController>>();
        _handlerMock = new Mock<IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse>>();
        _controller = new PromotionsController(_handlerMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GivenInvalidCountryCode_WhenGettingDiscounts_ThenThrowsValidationException()
    {
        //Arrange

        //Act
        var result = await _controller.Get(string.Empty, "ES", default);

        //Assert
    }
}
