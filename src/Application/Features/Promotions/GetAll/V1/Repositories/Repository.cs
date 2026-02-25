using PromotionEngine.Application.Shared;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;
//@TODO: delete
public class Repository
{
    private readonly DatabaseConnection _databaseConnectionInfo;

    public Repository(DatabaseConnection databaseConnectionInfo)
    {
        _databaseConnectionInfo = databaseConnectionInfo;
    }

    public IAsyncEnumerable<Promotion> GetAll(string countryCode, CancellationToken cancellationToken)
    {
        var databaseInfo = new DatabaseConnection("");
        return _databaseConnectionInfo.QueryAsync(_ => _.CountryCode == countryCode, cancellationToken);
    }
}
