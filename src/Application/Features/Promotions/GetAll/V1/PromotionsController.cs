using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Attributes;
using System.ComponentModel;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

[ApiController]
[Route("v{version:apiVersion}")]
[ApiVersion("1.0")]
public class PromotionsController : FeatureControllerBase
{
    private readonly IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse> _handler;

    public PromotionsController(
        IHandler<GetAllPromotionsRequest, GetAllPromotionsResponse> handler,
        ILogger<PromotionsController> logger) : base(logger)
    {
        _handler = handler;
    }

    [HttpGet("{countryCode}/promotions")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllPromotionsResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointSummary("GetPromotions")]
    [EndpointDescription("Get Promotions")]
    public async Task<IActionResult> Get(
        [CountryCode] string countryCode,
        [LanguageCode] string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new GetAllPromotionsRequest(countryCode, languageCode);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response switch
        {
            { ExceptionOccurred: true, Exception: var exception } => HandleException(exception!),
            _ => Ok(response)
        };
    }
}
