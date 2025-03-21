using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Models;

public class SonarrFile
{
	[JsonPropertyName("path")]
	public string Path { get; set; } = string.Empty;

	[JsonPropertyName("seriesId")]
	public int SeriesId { get; set; }

	[JsonPropertyName("episodeIds")]
	public int[] EpisodeIds { get; set; } = [];

	[JsonPropertyName("quality")]
	public SonarrQualityModel Quality { get; set; } = new();

	[JsonPropertyName("language")]
	public SonarrLanguage Language { get; set; } = new();
}
