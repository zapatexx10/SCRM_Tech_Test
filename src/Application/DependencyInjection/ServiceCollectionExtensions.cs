using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Promotion list
        services.AddTransient<IHandler<Features.Promotions.GetAll.V1.GetAllPromotionsRequest, Features.Promotions.GetAll.V1.GetAllPromotionsResponse>, Features.Promotions.GetAll.V1.GetAllPromotionsHandler>();

        return services;
    }
}
