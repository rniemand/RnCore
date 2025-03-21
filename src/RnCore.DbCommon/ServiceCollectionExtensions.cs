using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RnCore.DbCommon;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddMySqlConnectionFactory(this IServiceCollection services, IConfiguration configuration, string dbConnectionString)
	{
		services.TryAddSingleton<IDbConnectionFactory>(_ => new MySqlConnectionFactory(configuration, dbConnectionString));
		return services;
	}
}
