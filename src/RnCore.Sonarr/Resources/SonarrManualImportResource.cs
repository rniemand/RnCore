using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;
using RnCore.Sonarr.Models;

namespace RnCore.Sonarr.Resources;

public class SonarrManualImportResource
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("path")]
	public string? Path { get; set; }

	[JsonPropertyName("relativePath")]
	public string? RelativePath { get; set; }

	[JsonPropertyName("folderName")]
	public string? FolderName { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("size")]
	public long Size { get; set; }

	[JsonPropertyName("series")]
	public SonarrSeriesResource Series { get; set; } = new();

	[JsonPropertyName("seasonNumber")]
	public int? SeasonNumber { get; set; }

	[JsonPropertyName("episodes")]
	public SonarrEpisodeResource[]? Episodes { get; set; }

	[JsonPropertyName("episodeFileId")]
	public int? EpisodeFileId { get; set; }

	[JsonPropertyName("releaseGroup")]
	public string? ReleaseGroup { get; set; }

	[JsonPropertyName("quality")]
	public SonarrQualityModel Quality { get; set; } = new();

	[JsonPropertyName("languages")]
	public SonarrLanguage[]? Languages { get; set; }

	[JsonPropertyName("qualityWeight")]
	public int QualityWeight { get; set; }

	[JsonPropertyName("downloadId")]
	public string? DownloadId { get; set; }

	[JsonPropertyName("customFormats")]
	public SonarrCustomFormatResource[]? CustomFormats { get; set; }

	[JsonPropertyName("customFormatScore")]
	public int CustomFormatScore { get; set; }

	[JsonPropertyName("indexerFlags")]
	public int IndexerFlags { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("releaseType")]
	public SonarrReleaseType ReleaseType { get; set; } = SonarrReleaseType.Unknown;

	[JsonPropertyName("rejections")]
	public SonarrRejection[]? Rejections { get; set; }
}
