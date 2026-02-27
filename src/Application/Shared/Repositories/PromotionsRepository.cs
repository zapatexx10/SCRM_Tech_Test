using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;
using System.Runtime.CompilerServices;

namespace PromotionEngine.Application.Shared.Repositories;

public class PromotionsRepository : IPromotionsRepository
{

    private readonly IDatabaseConnectionFactory _connectionFactory;

    public PromotionsRepository(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    // I use the using just to ensure that everything is disposed after the call.
    public async Task<List<Promotion>> GetAll(string countryCode, CancellationToken cancellationToken)
    {
        using var databaseConnection = _connectionFactory.Create();

        var promotions = await databaseConnection
            .QueryAsync(_ => _.CountryCode == countryCode, cancellationToken)
            .ToListAsync(cancellationToken);

        return promotions;
    }

    //In the case of millions of promotions (not this case), we should consider using streaming instead of loading all promotions into memory at once.
    //In case of using this method we need to change the handler foreach ==> await foreach 
    //I use this await foreach here in order to keep the connection alive while looping through the promotions (because of the using), and to ensure that we are not loading all promotions into memory at once,
    //which can be beneficial when dealing with a large number of promotions.

    public async IAsyncEnumerable<Promotion> GetAllStreaming(string countryCode, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var databaseConnection = _connectionFactory.Create();

        await foreach (var promotion in databaseConnection
            .QueryAsync(_ => _.CountryCode == countryCode, cancellationToken)
            .WithCancellation(cancellationToken))
        {
            yield return promotion;
        }
    }

    public async IAsyncEnumerable<Promotion> GetAllStreamingFiltered(
    string countryCode,
    int maxItems,
    [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var databaseConnection = _connectionFactory.Create();
        var count = 0;

        await foreach (var promotion in databaseConnection
            .QueryAsync(_ => _.CountryCode == countryCode, cancellationToken)
            .WithCancellation(cancellationToken))
        {
            if (count >= maxItems)
                yield break;

            yield return promotion;
            count++;
        }
    }

    

    public async Task<Promotion?> GetByIdAsync(string countryCode, Guid promotionId, CancellationToken cancellationToken)
    {
        using var databaseConnection = _connectionFactory.Create();

        return await databaseConnection
        .QueryAsync(d => d.CountryCode == countryCode && d.Id == promotionId, cancellationToken)
        .FirstOrDefaultAsync(cancellationToken);
    }
}
