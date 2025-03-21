using System.Diagnostics;
using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Resources;

[DebuggerDisplay("{Name}")]
public class SonarrPostCommandResource
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = null!;
}
