using Dapper;
using RnCore.DbCommon;

namespace RnCore.DbConfiguration;

public interface IConfigurationRepo
{
	Task<IEnumerable<ConfigurationEntity>> GetAllConfigurationAsync(string hostName, CancellationToken cancellationToken = default);
	Task<IEnumerable<ConfigurationEntity>> GetAllConfigurationAsync(string hostName, string category, CancellationToken cancellationToken = default);
	Task<int> UpdateEntryAsync(ConfigurationEntity entry, CancellationToken cancellationToken = default);
	Task<int> InsertEntryAsync(ConfigurationEntity entry, CancellationToken cancellationToken = default);
}

public class ConfigurationRepo(IDbConnectionFactory connectionFactory) : IConfigurationRepo
{
	public async Task<IEnumerable<ConfigurationEntity>> GetAllConfigurationAsync(string hostName, CancellationToken cancellationToken = default)
	{
		using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
		return (await connection.QueryAsync<ConfigurationEntity>(new CommandDefinition(
			"""
			 SELECT *
			 FROM `Configuration` c
			 WHERE (c.HostName = @hostName OR c.HostName = '*')
			""",
			new { hostName },
			cancellationToken: cancellationToken
		))).AsList();
	}

	public async Task<IEnumerable<ConfigurationEntity>> GetAllConfigurationAsync(string hostName, string category, CancellationToken cancellationToken = default)
	{
		using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
		return (await connection.QueryAsync<ConfigurationEntity>(new CommandDefinition(
			"""
			 SELECT *
			 FROM `Configuration` c
			 WHERE (c.HostName = @hostName OR c.HostName = '*')
				AND c.Category = @category
			""",
			new { hostName, category },
			cancellationToken: cancellationToken
		))).AsList();
	}

	public async Task<int> UpdateEntryAsync(ConfigurationEntity entry, CancellationToken cancellationToken = default)
	{
		using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
		return await connection.ExecuteAsync(new CommandDefinition(
			"""
			UPDATE `Configuration`
			SET
				`ConfigValue` = @ConfigValue
			WHERE
				`HostName` = @HostName
				AND `ConfigKey` = @ConfigKey
				AND `Category` = @Category
				AND `IsCollection` = @IsCollection
				AND `ConfigType` = @ConfigType
			""",
			entry, cancellationToken: cancellationToken
		));
	}

	public async Task<int> InsertEntryAsync(ConfigurationEntity entry, CancellationToken cancellationToken = default)
	{
		using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
		return await connection.ExecuteAsync(new CommandDefinition(
			"""
			INSERT INTO `Configuration`
				(`HostName`,`ConfigKey`,`ConfigValue`,`Category`,`IsCollection`,`ConfigType`)
			VALUES
				(@HostName,@ConfigKey,@ConfigValue,@Category,@IsCollection,@ConfigType)
			""",
			entry, cancellationToken: cancellationToken
		));
	}
}
