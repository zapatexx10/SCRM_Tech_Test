using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Shared.Repositories;

public class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly string _dbConnectionString;

    //Also could use IOptions but due that we only have one value and I dont want to create a ConfigObject,
    //I think its better to get it directly from the configuration
    public DatabaseConnectionFactory(IConfiguration config)
    {
        _dbConnectionString = config.GetConnectionString("DbConnection")
                              ?? throw new InvalidOperationException("Database connection string is not configured.");
    }

    public DatabaseConnection Create()
    {
        return new DatabaseConnection(_dbConnectionString);
    }
}
