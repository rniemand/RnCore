namespace RnCore.Abstractions;

public interface IDirectoryInfoWrapper
{
	string Name { get; }
	IFileInfoWrapper[] GetFiles();
	IDirectoryInfoWrapper[] GetDirectories();
}

public class DirectoryInfoWrapper(string path) : IDirectoryInfoWrapper
{
	private readonly DirectoryInfo _directoryInfo = new(path);

	public string Name => _directoryInfo.Name;

	public IFileInfoWrapper[] GetFiles() => _directoryInfo
		.GetFiles()
		.Select(x => new FileInfoWrapper(x.FullName))
		.ToArray<IFileInfoWrapper>();

	public IDirectoryInfoWrapper[] GetDirectories() => _directoryInfo
		.GetDirectories()
		.Select(x => new DirectoryInfoWrapper(x.FullName))
		.ToArray<IDirectoryInfoWrapper>();
}
