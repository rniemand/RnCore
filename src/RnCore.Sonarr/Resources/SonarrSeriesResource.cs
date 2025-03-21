using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;
using RnCore.Sonarr.Models;
using RnCore.Sonarr.Options;

namespace RnCore.Sonarr.Resources;

public class SonarrSeriesResource
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("alternateTitles")]
	public SonarrAlternativeTitleResource[]? AlternateTitles { get; set; }

	[JsonPropertyName("sortTitle")]
	public string? SortTitle { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("status")]
	public SonarrSeriesStatusType Status { get; set; } = SonarrSeriesStatusType.Continuing;

	[JsonPropertyName("ended")]
	public bool Ended { get; set; }

	[JsonPropertyName("profileName")]
	public string? ProfileName { get; set; }

	[JsonPropertyName("overview")]
	public string? Overview { get; set; }

	[JsonPropertyName("nextAiring")]
	public DateTime? NextAiring { get; set; }

	[JsonPropertyName("previousAiring")]
	public DateTime? PreviousAiring { get; set; }

	[JsonPropertyName("network")]
	public string? Network { get; set; }

	[JsonPropertyName("airTime")]
	public string? AirTime { get; set; }

	[JsonPropertyName("images")]
	public SonarrMediaCover[]? Images { get; set; }

	[JsonPropertyName("originalLanguage")]
	public SonarrLanguage? OriginalLanguage { get; set; }

	[JsonPropertyName("remotePoster")]
	public string? RemotePoster { get; set; }

	[JsonPropertyName("seasons")]
	public SonarrSeasonResource[] Seasons { get; set; } = [];

	[JsonPropertyName("year")]
	public int Year { get; set; }

	[JsonPropertyName("path")]
	public string? Path { get; set; }

	[JsonPropertyName("qualityProfileId")]
	public int QualityProfileId { get; set; }

	[JsonPropertyName("languageProfileId")]
	public int LanguageProfileId { get; set; }

	[JsonPropertyName("seasonFolder")]
	public bool SeasonFolder { get; set; }

	[JsonPropertyName("monitored")]
	public bool Monitored { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("monitorNewItems")]
	public SonarrNewItemMonitorTypes MonitorNewItems { get; set; } = SonarrNewItemMonitorTypes.All;

	[JsonPropertyName("useSceneNumbering")]
	public bool UseSceneNumbering { get; set; }

	[JsonPropertyName("runtime")]
	public int Runtime { get; set; }

	[JsonPropertyName("tvdbId")]
	public int TvdbId { get; set; }

	[JsonPropertyName("tvRageId")]
	public int TvRageId { get; set; }

	[JsonPropertyName("tvMazeId")]
	public int TvMazeId { get; set; }

	[JsonPropertyName("tmdbId")]
	public int TmdbId { get; set; }

	[JsonPropertyName("firstAired")]
	public DateTime? FirstAired { get; set; }

	[JsonPropertyName("lastAired")]
	public DateTime? LastAired { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("seriesType")]
	public SonarrSeriesTypes SeriesType { get; set; } = SonarrSeriesTypes.Standard;

	[JsonPropertyName("cleanTitle")]
	public string? CleanTitle { get; set; }

	[JsonPropertyName("imdbId")]
	public string? ImdbId { get; set; }

	[JsonPropertyName("titleSlug")]
	public string? TitleSlug { get; set; }

	[JsonPropertyName("rootFolderPath")]
	public string? RootFolderPath { get; set; }

	[JsonPropertyName("folder")]
	public string? Folder { get; set; }

	[JsonPropertyName("certification")]
	public string? Certification { get; set; }

	[JsonPropertyName("genres")]
	public string[]? Genres { get; set; }

	[JsonPropertyName("tags")]
	public int[]? Tags { get; set; }

	[JsonPropertyName("added")]
	public DateTime Added { get; set; }

	[JsonPropertyName("addOptions")]
	public SonarrAddSeriesOptions AddOptions { get; set; } = new();

	[JsonPropertyName("ratings")]
	public SonarrRatings Ratings { get; set; } = new();

	[JsonPropertyName("statistics")]
	public SonarrSeriesStatisticsResource Statistics { get; set; } = new();

	[JsonPropertyName("episodesChanged")]
	public bool? EpisodesChanged { get; set; }
}
