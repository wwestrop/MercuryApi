using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MercuryApi.Extensions
{
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddMercuryApi(this IServiceCollection services) {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RequestScopedStorage>();

            return services;
        }
    }
}
