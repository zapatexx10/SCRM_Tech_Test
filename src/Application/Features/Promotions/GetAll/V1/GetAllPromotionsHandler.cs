using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Application.Shared.Mappings;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public class GetAllPromotionsHandler : IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse>
{
    private readonly IPromotionsRepository _repository;

    public GetAllPromotionsHandler(IPromotionsRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllPromotionsResponse> HandleAsync(GetAllPromotionsRequest request, CancellationToken cancellationToken = default)
    {
        var response = new GetAllPromotionsResponse();

        try
        {
            var promotions = await _repository.GetAll(request.CountryCode, cancellationToken).ToListAsync(cancellationToken);

            var promotionModels = new List<PromotionModel>();

            foreach (var promotion in promotions)
            {
                promotionModels.Add(promotion.ToPromotionModel(request.CountryCode));
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
