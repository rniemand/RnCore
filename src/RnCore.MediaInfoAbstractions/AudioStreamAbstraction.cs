using MediaInfo.Model;

namespace RnCore.MediaInfoAbstractions;

public interface IAudioStreamAbstraction : ILanguageMediaStreamAbstraction
{
	string AudioChannelsFriendly { get; }
	double Bitrate { get; }
	BitrateMode BitrateMode { get; }
	int Channel { get; }
	AudioCodec Codec { get; }
	string CodecFriendly { get; }
	TimeSpan Duration { get; }
	string Format { get; }
	double SamplingRate { get; }
}

public class AudioStreamAbstraction(AudioStream stream) : IAudioStreamAbstraction
{
	public string AudioChannelsFriendly => stream.AudioChannelsFriendly;
	public double Bitrate => stream.Bitrate;
	public BitrateMode BitrateMode => stream.BitrateMode;
	public int Channel => stream.Channel;
	public AudioCodec Codec => stream.Codec;
	public string CodecFriendly => stream.CodecFriendly;
	public TimeSpan Duration => stream.Duration;
	public string Format => stream.Format;
	public double SamplingRate => stream.SamplingRate;
	public bool Default => stream.Default;
	public bool Forced => stream.Forced;
	public string Language => stream.Language;
	public int Lcid => stream.Lcid;
	public long StreamSize => stream.StreamSize;
	public int Id => stream.Id;
	public int StreamNumber => stream.StreamNumber;
	public int StreamPosition => stream.StreamPosition;
}
