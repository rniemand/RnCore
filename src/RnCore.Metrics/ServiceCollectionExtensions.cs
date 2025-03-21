using Microsoft.Extensions.DependencyInjection;

namespace RnCore.Metrics;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRnCoreMetricsBase(this IServiceCollection services, bool useDefaultConfigProvider = true)
	{
		if (useDefaultConfigProvider)
			services.AddSingleton<IMetricsConfigProvider, MetricsConfigProvider>();

		services.AddSingleton<IMetricsService, MetricsService>();
		services.AddSingleton<IMetricOutput, ConsoleMetricOutput>();

		return services;
	}
}
