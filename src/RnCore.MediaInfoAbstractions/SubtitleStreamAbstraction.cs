using MediaInfo.Model;

namespace RnCore.MediaInfoAbstractions;

public interface ISubtitleStreamAbstraction : ILanguageMediaStreamAbstraction
{
	string Format { get; }
	SubtitleCodec Codec { get; }
}

public class SubtitleStreamAbstraction(SubtitleStream stream) : ISubtitleStreamAbstraction
{
	public int Id => stream.Id;
	public int StreamNumber => stream.StreamNumber;
	public int StreamPosition => stream.StreamPosition;
	public bool Default => stream.Default;
	public bool Forced => stream.Forced;
	public string Language => stream.Language;
	public int Lcid => stream.Lcid;
	public long StreamSize => stream.StreamSize;
	public string Format => stream.Format;
	public SubtitleCodec Codec => stream.Codec;
}
