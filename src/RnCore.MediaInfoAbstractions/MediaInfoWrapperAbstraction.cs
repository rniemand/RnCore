using MediaInfo;

namespace RnCore.MediaInfoAbstractions;

public interface IMediaInfoWrapperAbstraction
{
	IList<IAudioStreamAbstraction> AudioStreams { get; }
	IList<IVideoStreamAbstraction> VideoStreams { get; }
	IList<ISubtitleStreamAbstraction> Subtitles { get; }
}

public class MediaInfoWrapperAbstraction(MediaInfoWrapper mediaInfo) : IMediaInfoWrapperAbstraction
{
	public IList<IAudioStreamAbstraction> AudioStreams => mediaInfo.AudioStreams
		.Select(x => new AudioStreamAbstraction(x))
		.ToList<IAudioStreamAbstraction>();

	public IList<IVideoStreamAbstraction> VideoStreams => mediaInfo.VideoStreams
		.Select(x => new VideoStreamAbstraction(x))
		.ToList<IVideoStreamAbstraction>();

	public IList<ISubtitleStreamAbstraction> Subtitles => mediaInfo.Subtitles
		.Select(x => new SubtitleStreamAbstraction(x))
		.ToList<ISubtitleStreamAbstraction>();
}

