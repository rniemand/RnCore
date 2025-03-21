using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Resources;

public class SonarrAlternativeTitleResource
{
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("seasonNumber")]
	public int? SeasonNumber { get; set; }

	[JsonPropertyName("sceneSeasonNumber")]
	public int? SceneSeasonNumber { get; set; }

	[JsonPropertyName("sceneOrigin")]
	public string? SceneOrigin { get; set; }

	[JsonPropertyName("comment")]
	public string? Comment { get; set; }
}
