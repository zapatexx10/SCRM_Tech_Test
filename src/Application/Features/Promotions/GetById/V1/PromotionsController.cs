using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Attributes;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

[ApiController]
[Route("v{version:apiVersion}")]
[ApiVersion("1.0")]
public class PromotionsController : FeatureControllerBase
{
    private readonly IHandler<GetPromotionByIdRequest, GetPromotionByIdResponse> _handler;

    public PromotionsController(
        IHandler<GetPromotionByIdRequest, GetPromotionByIdResponse> handler,
        ILogger<PromotionsController> logger) : base(logger)
    {
        _handler = handler;
    }

    [HttpGet("{countryCode}/promotions/{promotionId:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPromotionByIdResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("GetPromotionById")]
    [EndpointDescription("Get Promotion By Id")]
    public async Task<IActionResult> GetById(
        [CountryCode] string countryCode,
        [LanguageCode] string languageCode,
        Guid promotionId,
        CancellationToken cancellationToken)
    {
        var request = new GetPromotionByIdRequest(countryCode, languageCode, promotionId);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response switch
        {
            { ExceptionOccurred: true, Exception: var exception } => HandleException(exception!),
            _ => Ok(response)
        };
    }
}
