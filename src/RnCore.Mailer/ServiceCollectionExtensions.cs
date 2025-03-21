using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RnCore.Mailer;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRnCoreMailer(this IServiceCollection services, IConfiguration configuration)
	{
		services.TryAddSingleton<IRnMailerFactory, RnMailerFactory>();

		return services;
	}
}
