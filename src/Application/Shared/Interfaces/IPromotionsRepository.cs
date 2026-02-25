using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Interfaces;

public interface IPromotionsRepository
{
    IAsyncEnumerable<Promotion> GetAll(string countryCode, CancellationToken cancellationToken);
}
