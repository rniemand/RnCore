using System.Diagnostics.CodeAnalysis;

namespace RnCore.Abstractions;

public interface IDirectoryAbstraction
{
	bool Exists([NotNullWhen(true)] string? path);
	DirectoryInfo CreateDirectory(string path);
}

public class DirectoryAbstraction : IDirectoryAbstraction
{
	public bool Exists([NotNullWhen(true)] string? path) => Directory.Exists(path);
	public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);
}
