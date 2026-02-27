using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Interfaces;

public interface IPromotionsRepository
{
    Task<List<Promotion>> GetAll(string countryCode, CancellationToken cancellationToken);

    IAsyncEnumerable<Promotion> GetAllStreaming(
        string countryCode,
        CancellationToken cancellationToken);

    IAsyncEnumerable<Promotion> GetAllStreamingFiltered(
        string countryCode,
        int maxItems,
        CancellationToken cancellationToken);

    Task<Promotion?> GetByIdAsync(
        string countryCode,
        Guid promotionId,
        CancellationToken cancellationToken);
}
