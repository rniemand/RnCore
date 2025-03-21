using System.Text.Json.Serialization;
using RnCore.Sonarr.Enums;

namespace RnCore.Sonarr.Models;

public class SonarrMediaCover
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	[JsonPropertyName("coverType")]
	public SonarrMediaCoverTypes CoverType { get; set; } = SonarrMediaCoverTypes.Unknown;

	[JsonPropertyName("url")]
	public string? Url { get; set; }

	[JsonPropertyName("remoteUrl")]
	public string? RemoteUrl { get; set; }
}
