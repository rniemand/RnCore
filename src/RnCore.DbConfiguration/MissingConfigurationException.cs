namespace RnCore.DbConfiguration;

public class MissingConfigurationException(string category, string key)
	: Exception($"DB Configuration value '{category}':'{key}' is required")
{
	public string Category { get; } = category;
	public string Key { get; } = key;
}
