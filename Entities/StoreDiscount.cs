namespace PromotionEngine.Entities;
public class StoreDiscount : Discount
{
    public override DiscountType Type => DiscountType.Store;

    public decimal? OriginalPrice { get; set; }

    public decimal? FinalPrice { get; set; }

    public decimal? LowestPriceLast30Days { get; set; }

    public string? PriceType { get; set; }

    public int UnitsToBuy { get; set; }

    public int UnitsToPay { get; set; }

    public bool HasPrice { get; set; }
}
