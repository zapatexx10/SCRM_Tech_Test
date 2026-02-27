using PromotionEngine.Application.Features.Promotions.GetAll.V1;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.GetAll.V1;

public class GetAllHandlerTests
{
    private readonly Mock<IPromotionsRepository> _repositoryMock;
    private readonly GetAllPromotionsHandler _getAllPromotionsHandler;

    public GetAllHandlerTests()
    {
        _repositoryMock = new Mock<IPromotionsRepository>();
        _getAllPromotionsHandler = new GetAllPromotionsHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task GivenRepositoryReturnsPromotions_WhenHandleAsyncIsCalled_ThenReturnsSuccessWithPromotions()
    {
        // Arrange
        var countryCode = "ES";
        var languageCode = "EN";
        var request = new GetAllPromotionsRequest(countryCode, languageCode);
        var promotions = GetSamplePromotions();

        _repositoryMock.Setup(r => r.GetAllStreaming(request.CountryCode, default))
               .Returns(promotions.ToAsyncEnumerable());

        // Act
        var response = await _getAllPromotionsHandler.HandleAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.ExceptionOccurred);
        Assert.Equal(2, response.Promotions.Count());
    }

    [Fact]
    public async Task GivenNoDisplayContentForLanguage_WhenHandleAsync_ThenResponseContainsException()
    {
        // Arrange
        var countryCode = "ES";
        var languageCode = "DE";
        var request = new GetAllPromotionsRequest(countryCode, languageCode);
        var promotions = GetSamplePromotions();

        _repositoryMock.Setup(r => r.GetAllStreaming(request.CountryCode, default))
               .Returns(promotions.ToAsyncEnumerable());

        // Act
        var response = await _getAllPromotionsHandler.HandleAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.ExceptionOccurred);
    }

    public static List<Promotion> GetSamplePromotions()
    {
        return new List<Promotion>
        {
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "ES",
                CreatedDate = DateTime.Now,
                Images = new List<string>() {"https://placehold.co/750x562?text=Oferta+especial"},
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
            },
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "ES",
                CreatedDate = DateTime.Now,
                Images = new List<string>() {"https://placehold.co/750x562?text=Special+offer"},
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
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
            }
        };
    }

}
