using System.Text.RegularExpressions;

namespace RnCore.DbConfiguration;

public class NlpDbConfigCollection : INlpDbConfigCollection
{
	private readonly Dictionary<string, ConfigurationEntity> _config = new(StringComparer.InvariantCultureIgnoreCase);
	private readonly Dictionary<string, List<ConfigurationEntity>> _collectionConfig = new(StringComparer.InvariantCultureIgnoreCase);

	public NlpDbConfigCollection() { }

	public NlpDbConfigCollection(List<ConfigurationEntity> config) : this()
	{
		foreach (var currentConfig in config.Where(c => c.HostName == "*"))
		{
			if (currentConfig.IsCollection)
			{
				if (!_collectionConfig.ContainsKey(currentConfig.ConfigKey))
					_collectionConfig[currentConfig.ConfigKey] = [];
				_collectionConfig[currentConfig.ConfigKey].Add(currentConfig);
			}
			else
			{
				_config[currentConfig.ConfigKey] = currentConfig;
			}
		}

		foreach (var currentConfig in config.Where(c => c.HostName != "*"))
		{
			if (currentConfig.IsCollection)
			{
				if (!_collectionConfig.ContainsKey(currentConfig.ConfigKey))
					_collectionConfig[currentConfig.ConfigKey] = [];
				_collectionConfig[currentConfig.ConfigKey].Add(currentConfig);
			}
			else
			{
				_config[currentConfig.ConfigKey] = currentConfig;
			}
		}
	}

	public bool HasStringValue(string key)
	{
		if (_config.Count == 0)
			return false;

		if (!_config.TryGetValue(key, out var value))
			return false;

		return value.ConfigType.Equals(DbValueType.String, StringComparison.InvariantCultureIgnoreCase);
	}

	public bool HasFilePathValue(string key)
	{
		if (_config.Count == 0)
			return false;

		if (!_config.TryGetValue(key, out var value))
			return false;

		return value.ConfigType.Equals(DbValueType.FilePath, StringComparison.InvariantCultureIgnoreCase);
	}

	public bool HasIntValue(string key)
	{
		if (_config.Count == 0)
			return false;

		if (!_config.TryGetValue(key, out var value))
			return false;

		return value.ConfigType.Equals(DbValueType.Int, StringComparison.InvariantCultureIgnoreCase);
	}

	public bool HasBoolValue(string key)
	{
		if (_config.Count == 0)
			return false;

		if (!_config.TryGetValue(key, out var value))
			return false;

		return value.ConfigType.Equals(DbValueType.Boolean, StringComparison.InvariantCultureIgnoreCase);
	}

	public bool HasDateTimeValue(string key)
	{
		if (_config.Count == 0)
			return false;

		if (!_config.TryGetValue(key, out var value))
			return false;

		return value.ConfigType.Equals(DbValueType.DateTime, StringComparison.InvariantCultureIgnoreCase);
	}

	public bool HasStringCollection(string key) => _collectionConfig.ContainsKey(key);

	public List<string> GetStringCollection(string key) => !_collectionConfig.TryGetValue(key, out var value)
		? []
		: value.Select(entry => entry.ConfigValue).ToList();

	public List<Regex> GetRegexCollection(string key, RegexOptions rxOptions = RegexOptions.Compiled)
	{
		if (!_collectionConfig.TryGetValue(key, out var values))
			return [];

		// Ensure that all entries are of the correct / expected type
		var badValues = values
			.Where(x => !x.ConfigType.Equals(DbValueType.Regex, StringComparison.InvariantCultureIgnoreCase))
			.ToList();

		if (badValues.Count > 0)
			throw new RnDbConfigurationException($"Found {badValues.Count} non regex values for key '{key}'");

		// Ensure all entries are part of a collection
		var nonCollectionEntries = values.Where(x => !x.IsCollection).ToList();
		if (nonCollectionEntries.Count > 0)
			throw new RnDbConfigurationException($"Some entries for '{key}' are not part of a collection, please address.");

		// Convert and return the Regex collection to the caller
		return values
			.Select(entry => new Regex(entry.ConfigValue, rxOptions))
			.ToList();
	}

	public string GetStringValue(string key) => !HasStringValue(key) ? string.Empty : _config[key].ConfigValue;

	public string GetFilePathValue(string key) => !HasFilePathValue(key) ? string.Empty : _config[key].ConfigValue;

	public int GetIntValue(string key) => !HasIntValue(key) ? 0 : int.Parse(_config[key].ConfigValue);

	public bool GetBoolValue(string key, bool fallback) =>
		!HasBoolValue(key) ? fallback : bool.Parse(_config[key].ConfigValue);

	public DateTime GetDateTimeValue(string key, DateTime fallback) =>
		!HasDateTimeValue(key) ? fallback : DateTime.Parse(_config[key].ConfigValue);
}
