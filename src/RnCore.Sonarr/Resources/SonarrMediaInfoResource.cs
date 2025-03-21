using System.Text.Json.Serialization;

namespace RnCore.Sonarr.Resources;

public class SonarrMediaInfoResource
{
	[JsonPropertyName("id")]
	public int ID { get; set; }

	[JsonPropertyName("audioBitrate")]
	public long AudioBitrate { get; set; }

	[JsonPropertyName("audioChannels")]
	public double AudioChannels { get; set; }

	[JsonPropertyName("audioCodec")]
	public string? AudioCodec { get; set; }

	[JsonPropertyName("audioLanguages")]
	public string? AudioLanguages { get; set; }

	[JsonPropertyName("audioStreamCount")]
	public int AudioStreamCount { get; set; }

	[JsonPropertyName("videoBitDepth")]
	public int VideoBitDepth { get; set; }

	[JsonPropertyName("videoBitrate")]
	public long VideoBitrate { get; set; }

	[JsonPropertyName("videoCodec")]
	public string? VideoCodec { get; set; }

	[JsonPropertyName("videoFps")]
	public double VideoFps { get; set; }

	[JsonPropertyName("videoDynamicRange")]
	public string? VideoDynamicRange { get; set; }

	[JsonPropertyName("videoDynamicRangeType")]
	public string? VideoDynamicRangeType { get; set; }

	[JsonPropertyName("resolution")]
	public string? Resolution { get; set; }

	[JsonPropertyName("runTime")]
	public string? RunTime { get; set; }

	[JsonPropertyName("scanType")]
	public string? ScanType { get; set; }

	[JsonPropertyName("subtitles")]
	public string? Subtitles { get; set; }
}
