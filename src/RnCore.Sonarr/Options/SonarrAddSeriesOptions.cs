using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;

namespace RnCore.Sonarr.Options;

public class SonarrAddSeriesOptions
{
	[JsonPropertyName("ignoreEpisodesWithFiles")]
	public bool IgnoreEpisodesWithFiles { get; set; }

	[JsonPropertyName("ignoreEpisodesWithoutFiles")]
	public bool IgnoreEpisodesWithoutFiles { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("monitor")]
	public SonarrMonitorTypes Monitor { get; set; } = SonarrMonitorTypes.Unknown;

	[JsonPropertyName("searchForMissingEpisodes")]
	public bool SearchForMissingEpisodes { get; set; }

	[JsonPropertyName("searchForCutoffUnmetEpisodes")]
	public bool SearchForCutoffUnmetEpisodes { get; set; }
}
