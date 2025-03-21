namespace RnCore.MediaInfoAbstractions;

public interface IMediaStreamAbstraction
{
	int Id { get; }
	int StreamNumber { get; }
	int StreamPosition { get; }
}
