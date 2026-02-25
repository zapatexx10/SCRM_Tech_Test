
namespace PromotionEngine.Entities;
public class OnlineDiscount : Discount
{
    public override DiscountType Type => DiscountType.Online;

    public decimal? OriginalPrice { get; set; }

    public decimal? FinalPrice { get; set; }

    public decimal? LowestPriceLast30Days { get; set; }

    public string? PriceType { get; set; }

    public int UnitsToBuy { get; set; }

    public int UnitsToPay { get; set; }

    public bool HasPrice { get; set; }
}
