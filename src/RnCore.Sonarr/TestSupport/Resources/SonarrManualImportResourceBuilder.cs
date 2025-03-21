using RnCore.Sonarr.Resources;

namespace RnCore.Sonarr.TestSupport.Resources;

public class SonarrManualImportResourceBuilder
{
	private readonly SonarrManualImportResource _resource = new();

	public SonarrManualImportResourceBuilder WithName(string name)
	{
		_resource.Name = name;
		return this;
	}

	public SonarrManualImportResourceBuilder WithSeries(SonarrSeriesResource series)
	{
		_resource.Series = series;
		return this;
	}

	public SonarrManualImportResource Build() => _resource;
}
