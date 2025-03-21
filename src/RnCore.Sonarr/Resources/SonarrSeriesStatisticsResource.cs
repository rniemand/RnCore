using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Resources;

public class SonarrSeriesStatisticsResource
{
	[JsonPropertyName("seasonCount")]
	public int SeasonCount { get; set; }

	[JsonPropertyName("episodeFileCount")]
	public int EpisodeFileCount { get; set; }

	[JsonPropertyName("episodeCount")]
	public int EpisodeCount { get; set; }

	[JsonPropertyName("totalEpisodeCount")]
	public int TotalEpisodeCount { get; set; }

	[JsonPropertyName("sizeOnDisk")]
	public long SizeOnDisk { get; set; }

	[JsonPropertyName("releaseGroups")]
	public string[]? ReleaseGroups { get; set; }

	[JsonPropertyName("percentOfEpisodes")]
	public double PercentOfEpisodes { get; set; }
}
