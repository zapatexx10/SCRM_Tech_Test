namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public record struct GetAllPromotionsRequest (string CountryCode, string LanguageCode);
