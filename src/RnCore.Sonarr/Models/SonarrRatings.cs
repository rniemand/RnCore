using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Models;

public class SonarrRatings
{
	[JsonPropertyName("votes")]
	public int Votes { get; set; }

	[JsonPropertyName("value")]
	public float Value { get; set; }
}
