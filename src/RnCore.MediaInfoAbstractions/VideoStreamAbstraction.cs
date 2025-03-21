using MediaInfo.Model;

namespace RnCore.MediaInfoAbstractions;

public interface IVideoStreamAbstraction : ILanguageMediaStreamAbstraction
{
	double Bitrate { get; }
	string Format { get; }
	VideoCodec Codec { get; }
	string Resolution { get; }
	int BitDepth { get; }
	string CodecName { get; }
	string CodecProfile { get; }
	ColorSpace ColorSpace { get; }
	TimeSpan Duration { get; }
	double FrameRate { get; }
	FrameRateMode FrameRateMode { get; }
	Hdr Hdr { get; }
	int Height { get; }
	bool Interlaced { get; }
	VideoStandard Standard { get; }
	StereoMode Stereoscopic { get; }
	ChromaSubSampling SubSampling { get; }
	TransferCharacteristic TransferCharacteristics { get; }
	int Width { get; }
}

public class VideoStreamAbstraction(VideoStream stream) : IVideoStreamAbstraction
{
	public int Id => stream.Id;
	public int StreamNumber => stream.StreamNumber;
	public int StreamPosition => stream.StreamPosition;
	public bool Default => stream.Default;
	public bool Forced => stream.Forced;
	public string Language => stream.Language;
	public int Lcid => stream.Lcid;
	public long StreamSize => stream.StreamSize;
	public double Bitrate => stream.Bitrate;
	public string Format => stream.Format;
	public VideoCodec Codec => stream.Codec;
	public string Resolution => stream.Resolution;
	public int BitDepth => stream.BitDepth;
	public string CodecName => stream.CodecName;
	public string CodecProfile => stream.CodecProfile;
	public ColorSpace ColorSpace => stream.ColorSpace;
	public TimeSpan Duration => stream.Duration;
	public double FrameRate => stream.FrameRate;
	public FrameRateMode FrameRateMode => stream.FrameRateMode;
	public Hdr Hdr => stream.Hdr;
	public int Height => stream.Height;
	public bool Interlaced => stream.Interlaced;
	public VideoStandard Standard => stream.Standard;
	public StereoMode Stereoscopic => stream.Stereoscopic;
	public ChromaSubSampling SubSampling => stream.SubSampling;
	public TransferCharacteristic TransferCharacteristics => stream.TransferCharacteristics;
	public int Width => stream.Width;
}
