namespace RnCore.Abstractions;

public interface IFilesystemObjectFactory
{
	IFileInfoWrapper GetFileInfo(string filePath);
	IDirectoryInfoWrapper GetDirectoryInfo(string path);
}

public class FilesystemObjectFactory : IFilesystemObjectFactory
{
	public IFileInfoWrapper GetFileInfo(string filePath) => new FileInfoWrapper(filePath);
	public IDirectoryInfoWrapper GetDirectoryInfo(string path) => new DirectoryInfoWrapper(path);
}
