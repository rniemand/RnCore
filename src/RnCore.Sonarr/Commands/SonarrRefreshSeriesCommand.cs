using System.Text.Json.Serialization;
using RnCore.Sonarr.Resources;

namespace RnCore.Sonarr.Commands;

public class SonarrRefreshSeriesCommand : SonarrPostCommandResource
{
	[JsonPropertyName("seriesId")]
	public int SeriesId { get; set; }

	public SonarrRefreshSeriesCommand()
	{
		Name = SonarrApiCommand.RefreshSeries;
	}

	public SonarrRefreshSeriesCommand(int seriesId) : this()
	{
		SeriesId = seriesId;
	}
}
