using System.ComponentModel;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

[ApiController]
[Route("v1")]
public class PromotionsController : FeatureControllerBase
{
    private readonly IHandler<Request, Response> _handler;

    public PromotionsController(
        IHandler<Request, Response> handler,
        ILogger<PromotionsController> logger) : base(logger)
    {
        _handler = handler;
    }

    [HttpGet("{countryCode}/promotions")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EndpointSummary("GetPromotions")]
    [EndpointDescription("Get Promotions")]
    public async Task<IActionResult> Get(
        [Description("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new Request(countryCode, languageCode);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response switch
        {
            { ExceptionOccurred: true, Exception: var exception } => HandleException(exception!),
            _ => Ok(response)
        };
    }
}
