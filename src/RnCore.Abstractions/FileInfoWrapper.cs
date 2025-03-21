namespace RnCore.Abstractions;

public interface IFileInfoWrapper
{
	long Length { get; }
	string Name { get; }
	string Extension { get; }
	DateTime CreationTimeUtc { get; }
	DateTime LastWriteTimeUtc { get; }
}

public class FileInfoWrapper(string filePath) : IFileInfoWrapper
{
	private readonly FileInfo _fileInfo = new(filePath);
	public long Length => _fileInfo.Length;
	public string Name => _fileInfo.Name;
	public string Extension => _fileInfo.Extension;
	public DateTime CreationTimeUtc => _fileInfo.CreationTimeUtc;
	public DateTime LastWriteTimeUtc => _fileInfo.LastWriteTimeUtc;
}
