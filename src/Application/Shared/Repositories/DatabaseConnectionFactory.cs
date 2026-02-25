using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Shared.Repositories;

public class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly string _dbConnectionString;

    // I decided to create a factory for the DatabaseConnection because it can be useful in the future
    // if we need to create multiple connections or if we need to add some logic before creating the connection.
    //Also its easy to replace for a real database connection instead of a simulated one with the list

    //Also could use IOptions but due that we only have one value,
    //I think its better to get it directly from the configuration
    public DatabaseConnectionFactory(IConfiguration config)
    {
        _dbConnectionString = config.GetConnectionString("PromotionsDatabase")
                              ?? throw new InvalidOperationException("Database connection string is not configured.");
    }

    public DatabaseConnection Create()
    {
        return new DatabaseConnection(_dbConnectionString);
    }
}
