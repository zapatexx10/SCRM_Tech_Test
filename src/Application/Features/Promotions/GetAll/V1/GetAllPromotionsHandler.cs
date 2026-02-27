using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Application.Shared.Mappings;
using PromotionEngine.Application.Shared.Models;

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
            var normalizedCountryCode = request.CountryCode.Trim().ToUpperInvariant();
            var normalizedLanguageCode = request.LanguageCode.Trim().ToUpperInvariant();
            var promotionModels = new List<PromotionModel>();

            var promotionsStreaming = _repository.GetAllStreaming(normalizedCountryCode, cancellationToken);
            
            await foreach (var promotion in promotionsStreaming)
            {
                promotionModels.Add(promotion.ToPromotionModel(normalizedLanguageCode));
            }

            var totalCount = promotionModels.Count;

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
