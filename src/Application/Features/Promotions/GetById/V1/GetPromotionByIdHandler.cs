using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Application.Shared.Mappings;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

public class GetPromotionByIdHandler : IHandler<GetPromotionByIdRequest, GetPromotionByIdResponse>
{
    private readonly IPromotionsRepository _promotionsRepository;

    public GetPromotionByIdHandler(IPromotionsRepository repository)
    {
        _promotionsRepository = repository;
    }

    public async Task<GetPromotionByIdResponse> HandleAsync(GetPromotionByIdRequest request, CancellationToken cancellationToken = default)
    {
        var response = new GetPromotionByIdResponse();

        try
        {
            var normalizedCountryCode = request.CountryCode.Trim().ToUpperInvariant();
            var normalizedLanguageCode = request.LanguageCode.Trim().ToUpperInvariant();

            var promotion = await _promotionsRepository.GetByIdAsync(normalizedCountryCode, request.PromotionId, cancellationToken);

            if (promotion is null)
            {
                return response
                    .SetException(new KeyNotFoundException($"Promotion with ID {request.PromotionId} not found for country code {normalizedCountryCode}."));
            }

            var promotionModel = promotion.ToPromotionModel(normalizedLanguageCode);

            return response
                .SetPromotion(promotionModel);
        }
        catch (Exception ex)
        {
            return response
                .SetException(ex);
        }
    }
}
