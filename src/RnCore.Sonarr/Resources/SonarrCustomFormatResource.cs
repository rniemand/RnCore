using System.Text.Json.Serialization;
using RnCore.Sonarr.Schemas;

namespace RnCore.Sonarr.Resources;

public class SonarrCustomFormatResource
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("includeCustomFormatWhenRenaming")]
	public bool? IncludeCustomFormatWhenRenaming { get; set; }

	[JsonPropertyName("specifications")]
	public SonarrCustomFormatSpecificationSchema[]? Specifications { get; set; }
}
