using PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;
using PromotionEngine.Application.Shared;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public class Handler : IHandler<Request, Response>
{
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        var response = new Response();

        try
        {
            var databaseConnection = new DatabaseConnection("database://cloudserver.databases.azure:8074/promotion-engine@chj458$@djks");
            await databaseConnection.ConnectAsync(cancellationToken);
            
            var repository = new Repository(databaseConnection);

            IEnumerable<Promotion> promotions = await repository.GetAll(request.CountryCode, cancellationToken).ToListAsync(cancellationToken);

            var promotionModels = new List<PromotionModel>();
            foreach (var promotion in promotions)
            {
                var promotionModel = new PromotionModel {
                   PromotionId = promotion.Id,
                   EndValidityDate = promotion.EndValidityDate
                };
                
                if(promotion.Discounts != null && promotion.Discounts.Any())
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
                
                if(promotion.DisplayContent != null && promotion.DisplayContent.Any())
                {
                    var displayContent = promotion.DisplayContent[request.LanguageCode];
                    promotionModel.Texts = new PromotionTextsModel()
                    {
                        Description = displayContent.Description,
                        DiscountTitle = displayContent.DiscountTitle,
                        Title = displayContent.Title,
                        DiscountDescription = displayContent.DiscountDescription
                    };
                }

                promotionModels.Add(promotionModel);
            }

            return response
                .SetPromotions(promotionModels);
        }
        catch (Exception ex)
        {
            return response
                .SetException(ex);
        }
    }
}
