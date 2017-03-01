using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MercuryApi.Extensions
{
	public static class ServiceCollectionExtensions {

		/// <summary>
		/// Adds the supporting frameworks required for MercuryApi into ASP.NET's service collection.
		/// </summary>
		public static IServiceCollection AddMercuryApi(this IServiceCollection services) {
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			return services;
		}

	}
}
