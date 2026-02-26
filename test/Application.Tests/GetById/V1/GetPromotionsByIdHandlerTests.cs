using PromotionEngine.Application.Features.Promotions.GetById.V1;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.GetById.V1;

public class GetPromotionsByIdHandlerTests
{
    private readonly Mock<IPromotionsRepository> _repositoryMock;
    private readonly GetPromotionByIdHandler _getPromotionByIdHandler;

    public GetPromotionsByIdHandlerTests()
    {
        _repositoryMock = new Mock<IPromotionsRepository>();
        _getPromotionByIdHandler = new GetPromotionByIdHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task GivenRepositoryReturnPromotion_WhenHandleAsyncIsCalled_ThenReturnsSuccessWithExpectedPromotion()
    {
        // Arrange
        var promotion = GetSamplePromotion();
        var countryCode = "ES";
        var languageCode = "EN";
        var id = promotion.Id;
        var request = new GetPromotionByIdRequest(countryCode, languageCode, id);
        

        _repositoryMock.Setup(r => r.GetByIdAsync(request.CountryCode, id, default))
               .ReturnsAsync(promotion);

        // Act
        var response = await _getPromotionByIdHandler.HandleAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.ExceptionOccurred);
        Assert.NotNull(response.Promotion);
        Assert.Equal(id, response.Promotion.PromotionId);
    }

    [Fact]
    public async Task GivenNoDiscountFoundWithExpectedId_WhenHandleAsync_ThenResponseContainsException()
    {
        // Arrange
        var promotions = GetSamplePromotion();
        var countryCode = "ES";
        var languageCode = "DE";
        var id = Guid.NewGuid();
        var request = new GetPromotionByIdRequest(countryCode, languageCode, id);        

        _repositoryMock.Setup(r => r.GetByIdAsync(request.CountryCode, id, default))
               .ReturnsAsync(promotions);

        // Act
        var response = await _getPromotionByIdHandler.HandleAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.ExceptionOccurred);
    }

    public static Promotion GetSamplePromotion()
    {
        return
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "ES",
                CreatedDate = DateTime.Now,
                Images = new List<string>() { "https://placehold.co/750x562?text=Oferta+especial" },
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(1),

                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "ES",
                        new DisplayContent
                        {
                            Title = "Oferta especial",
                            Description = "Ahorra en tu próxima compra con esta promoción exclusiva.",
                            DiscountTitle = "10% de descuento",
                            DiscountDescription = "Válido en compras superiores a 50 €. No acumulable."
                        }
                    },
                    {
                        "EN",
                        new DisplayContent
                        {
                            Title = "Special offer",
                            Description = "Save on your next purchase with this exclusive promotion.",
                            DiscountTitle = "10% off",
                            DiscountDescription = "Valid on purchases over €50. Cannot be combined."
                        }
                    }
                },
                Discounts = new List<Discount>()
                {
                    new StoreDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    }
                }
            };
    }

}
