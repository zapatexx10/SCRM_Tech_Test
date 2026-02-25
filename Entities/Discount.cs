using PromotionEngine.Entities.Converters;
using System.Text.Json.Serialization;

namespace PromotionEngine.Entities;


[JsonConverter(typeof(PromotionDiscountJsonConverter))]
public abstract class Discount
{
    public abstract DiscountType Type { get; }

}
