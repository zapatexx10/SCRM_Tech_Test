using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Interfaces;

public interface IPromotionsRepository
{
    Task<List<Promotion>> GetAll(string countryCode, CancellationToken cancellationToken);

    IAsyncEnumerable<Promotion> GetAllStreaming(string countryCode, CancellationToken cancellationToken);

    Task<List<Promotion>> GetAllFiltered(string countryCode, int maxPromotions, CancellationToken cancellationToken);

    Task<Promotion?> GetByIdAsync(string countryCode, Guid promotionId, CancellationToken cancellationToken);
}
