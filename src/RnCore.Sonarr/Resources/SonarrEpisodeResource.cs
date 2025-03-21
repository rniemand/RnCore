using System.Text.Json.Serialization;
using RnCore.Sonarr.Models;

namespace RnCore.Sonarr.Resources;

public class SonarrEpisodeResource
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("seriesId")]
	public int SeriesId { get; set; }

	[JsonPropertyName("tvdbId")]
	public int TvdbId { get; set; }

	[JsonPropertyName("episodeFileId")]
	public int EpisodeFileId { get; set; }

	[JsonPropertyName("seasonNumber")]
	public int SeasonNumber { get; set; }

	[JsonPropertyName("episodeNumber")]
	public int EpisodeNumber { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("airDate")]
	public string? AirDate { get; set; }

	[JsonPropertyName("airDateUtc")]
	public DateTime? AirDateUtc { get; set; }

	[JsonPropertyName("runtime")]
	public int Runtime { get; set; }

	[JsonPropertyName("finaleType")]
	public string? FinaleType { get; set; }

	[JsonPropertyName("overview")]
	public string? Overview { get; set; }

	[JsonPropertyName("episodeFile")]
	public SonarrEpisodeFileResource EpisodeFile { get; set; } = new();

	[JsonPropertyName("hasFile")]
	public bool HasFile { get; set; }

	[JsonPropertyName("monitored")]
	public bool Monitored { get; set; }

	[JsonPropertyName("absoluteEpisodeNumber")]
	public int? AbsoluteEpisodeNumber { get; set; }

	[JsonPropertyName("sceneAbsoluteEpisodeNumber")]
	public int? SceneAbsoluteEpisodeNumber { get; set; }

	[JsonPropertyName("sceneEpisodeNumber")]
	public int? SceneEpisodeNumber { get; set; }

	[JsonPropertyName("sceneSeasonNumber")]
	public int? SceneSeasonNumber { get; set; }

	[JsonPropertyName("unverifiedSceneNumbering")]
	public bool UnverifiedSceneNumbering { get; set; }

	[JsonPropertyName("endTime")]
	public DateTime? EndTime { get; set; }

	[JsonPropertyName("grabDate")]
	public DateTime? GrabDate { get; set; }

	[JsonPropertyName("seriesTitle")]
	public string? SeriesTitle { get; set; }

	[JsonPropertyName("series")]
	public SonarrSeriesResource Series { get; set; } = new();

	[JsonPropertyName("images")]
	public SonarrMediaCover[]? Images { get; set; }
}
