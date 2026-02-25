using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Application.Shared.Repositories;

namespace PromotionEngine.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Promotion list
        services.AddTransient<IHandler<Features.Promotions.GetAll.V1.GetAllPromotionsRequest, Features.Promotions.GetAll.V1.GetAllPromotionsResponse>, Features.Promotions.GetAll.V1.GetAllPromotionsHandler>();
        services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        services.AddScoped<IPromotionsRepository, PromotionsRepository>();
        return services;
    }
}
