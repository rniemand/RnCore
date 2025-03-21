using Microsoft.Extensions.DependencyInjection;

namespace RnCore.Metrics.InfluxDB;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRnCoreMetricsInfluxDB(this IServiceCollection services, bool useDefaultConfigProvider = true)
	{
		if (useDefaultConfigProvider)
			services.AddSingleton<IInfluxDbOutputConfigProvider, InfluxDbOutputConfigProvider>();

		services.AddSingleton<IMetricOutput, InfluxDbMetricOutput>();

		return services;
	}
}
