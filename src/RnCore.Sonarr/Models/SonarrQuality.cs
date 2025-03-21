using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;

namespace RnCore.Sonarr.Models;

public class SonarrQuality
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("source")]
	public SonarrQualitySource Source { get; set; } = SonarrQualitySource.Unknown;

	[JsonPropertyName("resolution")]
	public int Resolution { get; set; }
}
