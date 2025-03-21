using RnCore.Sonarr.Resources;

namespace RnCore.Sonarr.TestSupport.Resources;

public class SonarrSeriesResourceBuilder
{
	private readonly SonarrSeriesResource _series = new();

	public SonarrSeriesResourceBuilder WithSeriesId(int id)
	{
		_series.Id = id;
		return this;
	}

	public SonarrSeriesResource Build() => _series;
}
