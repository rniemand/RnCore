using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Models;

public class SonarrLanguage
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }
}
