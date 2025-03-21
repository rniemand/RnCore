using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Models;

public class SonarrQualityModel
{
	[JsonPropertyName("quality")]
	public SonarrQuality Quality { get; set; } = new();

	[JsonPropertyName("revision")]
	public SonarrRevision Revision { get; set; } = new();
}
