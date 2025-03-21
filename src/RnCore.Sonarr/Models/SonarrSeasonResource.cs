using System.Text.Json.Serialization;
using RnCore.Sonarr.Resources;

namespace RnCore.Sonarr.Models;

public class SonarrSeasonResource
{
	[JsonPropertyName("seasonNumber")]
	public int SeasonNumber { get; set; }

	[JsonPropertyName("monitored")]
	public bool Monitored { get; set; }

	[JsonPropertyName("statistics")]
	public SonarrSeasonStatisticsResource[] Statistics { get; set; } = [];

	[JsonPropertyName("images")]
	public SonarrMediaCover[]? Images { get; set; }
}
