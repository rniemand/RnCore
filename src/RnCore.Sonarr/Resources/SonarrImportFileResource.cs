using System.Text.Json.Serialization;
using RnCore.Sonarr.Models;

namespace RnCore.Sonarr.Resources;

public class SonarrImportFileResource
{
	[JsonPropertyName("path")]
	public string? Path { get; set; }

	[JsonPropertyName("seriesId")]
	public int SeriesId { get; set; }

	[JsonPropertyName("episodeIds")]
	public int[] EpisodeIds { get; set; } = [];

	[JsonPropertyName("quality")]
	public SonarrQualityModel Quality { get; set; } = new();

	[JsonPropertyName("languages")]
	public SonarrLanguage[] Languages { get; set; } = [];

	[JsonPropertyName("indexerFlags")]
	public int IndexerFlags { get; set; }

	[JsonPropertyName("releaseType")]
	public string ReleaseType { get; set; } = null!;

	public static SonarrImportFileResource FromManualImportResource(SonarrManualImportResource resource) =>
		new()
		{
			EpisodeIds = resource.Episodes?.Select(x => x.ID).ToArray() ?? [],
			IndexerFlags = 0,
			Languages = resource.Languages ?? [],
			Path = resource.Path!,
			Quality = resource.Quality,
			ReleaseType = "singleEpisode",
			SeriesId = resource.Series.Id,
		};
}
