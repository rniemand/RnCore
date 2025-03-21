using System.Diagnostics;
using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;
using RnCore.Sonarr.Models;

namespace RnCore.Sonarr.Resources;

// https://sonarr.tv/docs/api/#/Command/post_api_v3_command
[DebuggerDisplay("{Name}")]
public class SonarrCommandResource
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("commandName")]
	public string? CommandName { get; set; }

	[JsonPropertyName("message")]
	public string? Message { get; set; }

	[JsonPropertyName("body")]
	public SonarrCommand Body { get; set; } = new();

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("priority")]
	public SonarrCommandPriority Priority { get; set; } = SonarrCommandPriority.Normal;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("status")]
	public SonarrCommandStatus Status { get; set; } = SonarrCommandStatus.Unknown;

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("result")]
	public SonarrCommandResult Result { get; set; } = SonarrCommandResult.Unknown;

	[JsonPropertyName("queued")]
	public DateTime Queued { get; set; }

	[JsonPropertyName("started")]
	public DateTime? Started { get; set; }

	[JsonPropertyName("ended")]
	public DateTime? Ended { get; set; }

	[JsonPropertyName("duration")]
	public string? Duration { get; set; }

	[JsonPropertyName("exception")]
	public string? Exception { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("trigger")]
	public SonarrCommandTrigger Trigger { get; set; } = SonarrCommandTrigger.Unspecified;

	[JsonPropertyName("clientUserAgent")]
	public string? ClientUserAgent { get; set; }

	[JsonPropertyName("stateChangeTime")]
	public DateTime? StateChangeTime { get; set; }

	[JsonPropertyName("sendUpdatesToClient")]
	public bool SendUpdatesToClient { get; set; }

	[JsonPropertyName("updateScheduledTask")]
	public bool UpdateScheduledTask { get; set; }

	[JsonPropertyName("lastExecutionTime")]
	public DateTime? LastExecutionTime { get; set; }
}
