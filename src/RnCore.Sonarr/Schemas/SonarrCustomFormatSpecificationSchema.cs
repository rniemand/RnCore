using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Schemas;

public class SonarrCustomFormatSpecificationSchema
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("implementation")]
	public string? Implementation { get; set; }

	[JsonPropertyName("implementationName")]
	public string? ImplementationName { get; set; }

	[JsonPropertyName("infoLink")]
	public string? InfoLink { get; set; }

	[JsonPropertyName("negate")]
	public bool Negate { get; set; }

	[JsonPropertyName("required")]
	public bool Required { get; set; }

	// https://sonarr.tv/docs/api/#/ManualImport/get_api_v3_manualimport
	// TODO: (SonarrApi) Complete implementation for "fields"
	// TODO: (SonarrApi) Complete implementation for "presets"
}
