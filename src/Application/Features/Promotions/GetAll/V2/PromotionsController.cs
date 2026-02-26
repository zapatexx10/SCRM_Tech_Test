using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

[ApiController]
[Route("v{version:apiVersion}")]
[ApiVersion("2.0")]
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
    [EndpointSummary("GetPromotions")]
    [EndpointDescription("Get Promotions")]
    public async Task<IActionResult> Get(
        [CountryCode] string countryCode,
        [LanguageCode] string languageCode,
        [Range(1, int.MaxValue)] int maxPromotions,
        CancellationToken cancellationToken)
    {
        var request = new GetAllPromotionsRequest(countryCode, languageCode, maxPromotions);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response switch
        {
            { ExceptionOccurred: true, Exception: var exception } => HandleException(exception!),
            _ => Ok(response)
        };
    }
}
