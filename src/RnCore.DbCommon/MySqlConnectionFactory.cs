using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace RnCore.DbCommon;

public class MySqlConnectionFactory(IConfiguration configuration, string connectionString) : IDbConnectionFactory
{
	public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
	{
		var connection = new MySqlConnection(connectionString);
		await connection.OpenAsync(cancellationToken);
		return connection;
	}

	public async Task<IDbConnection> CreateNamedConnectionAsync(string connectionName, CancellationToken cancellationToken = default)
	{
		var resolvedConnectionString = GetConnectionString(connectionName);
		var connection = new MySqlConnection(resolvedConnectionString);
		await connection.OpenAsync(cancellationToken);
		return connection;
	}

	private string GetConnectionString(string name)
	{
		var resolvedConnectionString = configuration[$"ConnectionStrings:{name}"];
		if (string.IsNullOrWhiteSpace(resolvedConnectionString))
			throw new Exception($"Unable to resolve connection string: {name}");
		return resolvedConnectionString;
	}
}
