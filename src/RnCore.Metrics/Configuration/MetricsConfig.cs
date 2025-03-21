using Microsoft.Extensions.Configuration;

namespace RnCore.Metrics;

public class MetricsConfig
{
	public const string ConfigKey = "RnCore.Metrics";

	[ConfigurationKeyName("enabled")]
	public bool Enabled { get; set; }

	[ConfigurationKeyName("application")]
	public string Application { get; set; } = string.Empty;

	[ConfigurationKeyName("template")]
	public string Template { get; set; } = "{app}/{measurement}";

	[ConfigurationKeyName("overrides")]
	public Dictionary<string, string> Overrides { get; set; } = new();

	[ConfigurationKeyName("environment")]
	public string Environment { get; set; } = "production";

	[ConfigurationKeyName("enableConsoleOutput")]
	public bool EnableConsoleOutput { get; set; } = false;
}
