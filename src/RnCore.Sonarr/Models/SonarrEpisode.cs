using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Models;

public class SonarrEpisode
{
	[JsonPropertyName("seriesId")]
	public int SeriesId { get; set; }

	[JsonPropertyName("tvdbId")]
	public int TVDBId { get; set; }

	[JsonPropertyName("episodeFileId")]
	public int EpisodeFileId { get; set; }

	[JsonPropertyName("seasonNumber")]
	public int SeasonNumber { get; set; }

	[JsonPropertyName("episodeNumber")]
	public int EpisodeNumber { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	[JsonPropertyName("airDate")]
	public string AirDate { get; set; } = string.Empty;

	[JsonPropertyName("airDateUtc")]
	public DateTime AirDateUtc { get; set; }

	[JsonPropertyName("overview")]
	public string Overview { get; set; } = string.Empty;

	[JsonPropertyName("hasFile")]
	public bool HasFile { get; set; }

	[JsonPropertyName("monitored")]
	public bool Monitored { get; set; }

	[JsonPropertyName("unverifiedSceneNumbering")]
	public bool UnverifiedSceneNumbering { get; set; }

	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("absoluteEpisodeNumber")]
	public int AbsoluteEpisodeNumber { get; set; }
}
