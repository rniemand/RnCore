using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;

namespace RnCore.Sonarr.Models;

public class SonarrCommand
{
	[JsonPropertyName("sendUpdatesToClient")]
	public bool SendUpdatesToClient { get; set; }

	[JsonPropertyName("updateScheduledTask")]
	public bool UpdateScheduledTask { get; set; }

	[JsonPropertyName("completionMessage")]
	public string? CompletionMessage { get; set; }

	[JsonPropertyName("requiresDiskAccess")]
	public bool RequiresDiskAccess { get; set; }

	[JsonPropertyName("isExclusive")]
	public bool IsExclusive { get; set; }

	[JsonPropertyName("isLongRunning")]
	public bool IsLongRunning { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("lastExecutionTime")]
	public DateTime? LastExecutionTime { get; set; }

	[JsonPropertyName("lastStartTime")]
	public DateTime? LastStartTime { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("trigger")]
	public SonarrCommandTrigger Trigger { get; set; } = SonarrCommandTrigger.Unspecified;

	[JsonPropertyName("suppressMessages")]
	public bool SuppressMessages { get; set; }

	[JsonPropertyName("clientUserAgent")]
	public string? ClientUserAgent { get; set; }
}
