namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

public record GetPromotionByIdRequest(
    string CountryCode,
    string LanguageCode,
    Guid PromotionId);
