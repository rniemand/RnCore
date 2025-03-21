using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RnCore.Ssh;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRnSsh(this IServiceCollection services)
	{
		services.TryAddSingleton<ISshClientFactory, SshClientFactory>();

		return services;
	}
}
