using PromotionEngine.Application.Shared.Models;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

public class GetAllPromotionsResponse
{
    public IEnumerable<PromotionModel> Promotions { get; private set; } = Enumerable.Empty<PromotionModel>();

    public int TotalCount { get; private set; }

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

    public GetAllPromotionsResponse SetPromotions(IEnumerable<PromotionModel> promotions, int totalCount)
    {
        Promotions = promotions;

        TotalCount = totalCount;

        return this;
    }
}
