using System.Diagnostics;

namespace RnCore.DbConfiguration;

[DebuggerDisplay("{HostName} ({Category}:{ConfigKey}) [{ConfigType}] {ConfigValue}")]
public class ConfigurationEntity
{
	public string HostName { get; set; } = "*";
	public required string ConfigKey { get; set; }
	public required string ConfigValue { get; set; }
	public string Category { get; set; } = "*";
	public bool IsCollection { get; set; }
	public string ConfigType { get; set; } = "unknown";
}
