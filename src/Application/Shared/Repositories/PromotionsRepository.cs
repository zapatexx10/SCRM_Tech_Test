using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Repositories;

public class PromotionsRepository : IPromotionsRepository
{

    private readonly IDatabaseConnectionFactory _connectionFactory;   

    public PromotionsRepository(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    // I use the using just to ensure that everything is disposed after the call.
    public IAsyncEnumerable<Promotion> GetAll(string countryCode, CancellationToken cancellationToken)
    {
        using var databaseConnection = _connectionFactory.Create();
        return databaseConnection.QueryAsync(_ => _.CountryCode == countryCode, cancellationToken);
    }
}
