using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Get Promotion list
        services.AddTransient<IHandler<Features.Promotions.GetAll.V1.Request, Features.Promotions.GetAll.V1.Response>, Features.Promotions.GetAll.V1.Handler>();

        return services;
    }
}
