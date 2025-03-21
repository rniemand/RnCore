using System.Text.RegularExpressions;

namespace RnCore.DbConfiguration;

public interface INlpDbConfigCollection
{
	bool HasStringValue(string key);
	bool HasFilePathValue(string key);
	bool HasIntValue(string key);
	bool HasBoolValue(string key);
	bool HasDateTimeValue(string key);
	bool HasStringCollection(string key);
	List<string> GetStringCollection(string key);
	List<Regex> GetRegexCollection(string key, RegexOptions rxOptions = RegexOptions.Compiled);
	string GetStringValue(string key);
	string GetFilePathValue(string key);
	int GetIntValue(string key);
	bool GetBoolValue(string key, bool fallback);
	DateTime GetDateTimeValue(string key, DateTime fallback);
}
