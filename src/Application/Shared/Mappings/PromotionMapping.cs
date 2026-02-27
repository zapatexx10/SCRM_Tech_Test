using PromotionEngine.Application.Shared.Models;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Mappings;

public static class PromotionMapping
{
    //I Created an extension method to map Promotion to PromotionModel
    //I think thats better just in case we use a library like Automapper
    //that later it turns by license out of nowhere (like Masstransit, Automapper or MediatR).. I suffered it in my own flesh
    public static PromotionModel ToPromotionModel(this Promotion promotion, string languageCode)
    {
        var promotionModel = new PromotionModel
        {
            PromotionId = promotion.Id,
            EndValidityDate = promotion.EndValidityDate
        };

        if (promotion.Discounts is not null && promotion.Discounts.Any())
        {
            promotionModel.Discounts = new List<Discount>();

            foreach (var discount in promotion.Discounts)
            {
                promotionModel.Discounts.Add(discount);
            }
        }

        foreach (var image in promotion.Images)
        {
            promotionModel.Images.Add(image);
        }

        if (promotion.DisplayContent != null && promotion.DisplayContent.Any())
        {
            var displayContent = promotion.DisplayContent[languageCode];

            promotionModel.Texts = new PromotionTextsModel()
            {
                Description = displayContent.Description,
                DiscountTitle = displayContent.DiscountTitle,
                Title = displayContent.Title,
                DiscountDescription = displayContent.DiscountDescription
            };
        }

        return promotionModel;
    }
}
