using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;

namespace RnCore.Sonarr.Models;

public class SonarrRejection
{
	[JsonPropertyName("reason")]
	public string? Reason { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("type")]
	public SonarrRejectionType Type { get; set; } = SonarrRejectionType.Permanent;
}
