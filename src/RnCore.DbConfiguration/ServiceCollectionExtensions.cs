using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RnCore.Logging;

namespace RnCore.DbConfiguration;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRnCoreDbConfiguration(this IServiceCollection services, string hostName)
	{
		services.TryAddSingleton<IConfigurationRepo, ConfigurationRepo>();

		services.TryAddSingleton<IDbConfigurationService>(serviceProvider => new DbConfigurationService(
			serviceProvider.GetRequiredService<ILoggerAdapter<DbConfigurationService>>(),
			serviceProvider.GetRequiredService<IConfigurationRepo>(),
			hostName
		));

		return services;
	}
}
