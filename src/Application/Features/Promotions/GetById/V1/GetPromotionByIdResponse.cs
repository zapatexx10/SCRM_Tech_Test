using PromotionEngine.Application.Shared.Models;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

public class GetPromotionByIdResponse
{
    public PromotionModel Promotion { get; private set; } = null!;

    [JsonIgnore]
    public bool ExceptionOccurred { get; private set; }

    [JsonIgnore]
    public Exception? Exception { get; private set; }


    public GetPromotionByIdResponse SetException(Exception ex)
    {
        ArgumentNullException.ThrowIfNull(ex, nameof(ex));

        ExceptionOccurred = true;
        Exception = ex;

        return this;
    }

    public GetPromotionByIdResponse SetPromotion(PromotionModel promotion)
    {
        Promotion = promotion;

        return this;
    }
}
