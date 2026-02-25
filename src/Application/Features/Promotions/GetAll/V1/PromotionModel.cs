using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public class PromotionModel
{
    public required Guid PromotionId { get; set; }
    public PromotionTextsModel? Texts { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public List<Discount>? Discounts { get; set; }
    public required DateTime EndValidityDate;
}

public class PromotionTextsModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DiscountTitle { get; set; }
    public string? DiscountDescription { get; set; }
}

