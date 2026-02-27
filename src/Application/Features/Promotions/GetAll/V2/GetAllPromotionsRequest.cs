namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

//Interesting the choice to use the struct  (faster and better for the GarbageCollector)
//Never used that before, but it makes sense for a request object that is immutable and likely to be short-lived.
public record struct GetAllPromotionsRequest (
    string CountryCode,
    string LanguageCode,
    int MaxPromotions);
