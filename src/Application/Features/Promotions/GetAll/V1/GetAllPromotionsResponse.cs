namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public class GetAllPromotionsResponse
{
    public IEnumerable<PromotionModel> Promotions { get; private set; } = Enumerable.Empty<PromotionModel>();

    [JsonIgnore]
    public bool ExceptionOccurred { get; private set; }

    [JsonIgnore]
    public Exception? Exception { get; private set; }
    
    public GetAllPromotionsResponse SetException(Exception ex)
    {
        ArgumentNullException.ThrowIfNull(ex, nameof(ex));

        ExceptionOccurred = true;
        Exception = ex;

        return this;
    }

    public GetAllPromotionsResponse SetPromotions(IEnumerable<PromotionModel> promotions)
    {
        Promotions = promotions;

        return this;
    }
}
