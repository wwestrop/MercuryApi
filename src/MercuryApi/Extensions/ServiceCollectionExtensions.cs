using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MercuryApi.Extensions
{
	public static class ServiceCollectionExtensions {

		// TODO: I don't believe this library's end-consumer will need this, as the public ctor news up all the required concrete types
		// TODO: However, adding ASP's IHttpContextContextAccessor via this call might be a nice convenience. 
		public static IServiceCollection AddMercuryApi(this IServiceCollection services) {
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			return services;
		}

	}
}
