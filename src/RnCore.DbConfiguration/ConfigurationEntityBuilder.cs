namespace RnCore.DbConfiguration;

public class ConfigurationEntityBuilder(string category)
{
	public ConfigurationEntity FromDateTime(string key, DateTime value, string? hostName = null) => new()
	{
		ConfigKey = key,
		ConfigValue = value.ToString("O"),
		ConfigType = DbValueType.DateTime,
		Category = category,
		IsCollection = false,
		HostName = hostName ?? string.Empty
	};
}
