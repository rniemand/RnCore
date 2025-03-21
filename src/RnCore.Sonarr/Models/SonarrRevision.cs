using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Models;

public class SonarrRevision
{
	[JsonPropertyName("version")]
	public int Version { get; set; }

	[JsonPropertyName("real")]
	public int Real { get; set; }

	[JsonPropertyName("isRepack")]
	public bool IsRepack { get; set; }
}
