using System.Runtime.CompilerServices;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared;

/// <summary>
/// DISCLAIMER: YOU CAN'T MODIFY THIS FILE, THIS IS BEEING USED TO SIMULATE A DATABASE
/// </summary>
#pragma warning disable CS9113 // Parameter is unread.
public class DatabaseConnection(string connectionString) : IDisposable
#pragma warning restore CS9113 // Parameter is unread.
{
    public async IAsyncEnumerable<Promotion> QueryAsync(Func<Promotion, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var promotion in promotions.Where(predicate))
        {
            if (cancellationToken.IsCancellationRequested)
                break;
            
            await Task.Yield();
            yield return promotion;
        }
    }

    public Task ConnectAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<bool> PingAsync()
    {
        return Task.FromResult(true);
    }

    public void Dispose()
    {
        // example purposes
    }
    
    static readonly List<Promotion> promotions = new List<Promotion>() {

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
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = new List<string>() {"https://placehold.co/750x562?text=Special+offer"},
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "DE",
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
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = new List<string>() {"https://placehold.co/750x562?text=Sonderangebot"},
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        //In the frontend appears this DisplayContent when we are searching for DE country and DE language offers, but the strings are in english...
                        "DE",
                        new DisplayContent
                        {
                            Title = "Sonderangebot",
                            Description = "Sparen Sie bei Ihrem nächsten Einkauf mit dieser exklusiven Aktion.",
                            DiscountTitle = "10 % Rabatt",
                            DiscountDescription = "Gültig ab einem Einkaufswert von 50 €. Nicht kombinierbar."
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
                    },
                    new OnlineDiscount()
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
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = new List<string>() {"https://placehold.co/750x562?text=Exklusiver+Preisvorteil"},
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Disabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "DE",
                        new DisplayContent
                        {
                            Title = "Exklusiver Preisvorteil",
                            Description = "Profitieren Sie von dieser zeitlich begrenzten Aktion und sichern Sie sich einen Vorteil bei Ihrem Einkauf.",
                            DiscountTitle = "5 € Sofortrabatt",
                            DiscountDescription = "Der Rabatt wird automatisch an der Kasse abgezogen. Gültig bis zum 31.12."
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
                    },
                    new OnlineDiscount()
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
