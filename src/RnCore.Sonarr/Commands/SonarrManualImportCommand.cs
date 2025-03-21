using System.Text.Json.Serialization;
using RnCore.Sonarr.Resources;

namespace RnCore.Sonarr.Commands;

public class SonarrManualImportCommand : SonarrPostCommandResource
{
	[JsonPropertyName("importMode")]
	public string ImportMode { get; set; }

	[JsonPropertyName("files")]
	public SonarrImportFileResource[] Files { get; set; } = [];

	public SonarrManualImportCommand()
	{
		Name = SonarrApiCommand.ManualImport;
		ImportMode = "move";
	}
}
