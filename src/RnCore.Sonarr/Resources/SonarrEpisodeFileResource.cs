using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;
using RnCore.Sonarr.Models;

namespace RnCore.Sonarr.Resources;

public class SonarrEpisodeFileResource
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("seriesId")]
	public int SeriesId { get; set; }

	[JsonPropertyName("seasonNumber")]
	public int SeasonNumber { get; set; }

	[JsonPropertyName("relativePath")]
	public string? RelativePath { get; set; }

	[JsonPropertyName("path")]
	public string? Path { get; set; }

	[JsonPropertyName("size")]
	public long Size { get; set; }

	[JsonPropertyName("dateAdded")]
	public DateTime DateAdded { get; set; }

	[JsonPropertyName("sceneName")]
	public string? SceneName { get; set; }

	[JsonPropertyName("releaseGroup")]
	public string? ReleaseGroup { get; set; }

	[JsonPropertyName("languages")]
	public SonarrLanguage[]? Languages { get; set; }

	[JsonPropertyName("quality")]
	public SonarrQualityModel Quality { get; set; } = new();

	[JsonPropertyName("customFormats")]
	public SonarrCustomFormatResource[]? CustomFormats { get; set; }

	[JsonPropertyName("customFormatScore")]
	public int CustomFormatScore { get; set; }

	[JsonPropertyName("indexerFlags")]
	public int? IndexerFlags { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("releaseType")]
	public SonarrReleaseType ReleaseType { get; set; } = SonarrReleaseType.Unknown;

	[JsonPropertyName("mediaInfo")]
	public SonarrMediaInfoResource MediaInfo { get; set; } = new();

	[JsonPropertyName("qualityCutoffNotMet")]
	public bool QualityCutoffNotMet { get; set; }
}
