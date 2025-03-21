using System.Diagnostics.CodeAnalysis;

namespace RnCore.Abstractions;

public interface IPathAbstraction
{
	string Combine(params string[] paths);
	string Combine(string path1, string path2);
	string? GetDirectoryName(string? path);
	string? GetFileName(string? path);
	string? GetFileNameWithoutExtension(string? path);
	string? GetExtension(string? path);
	string Join(string? path1, string? path2);
	bool EndsInDirectorySeparator([NotNullWhen(true)] string? path);
}

public class PathAbstraction : IPathAbstraction
{
	public string Combine(params string[] paths) => Path.Combine(paths);
	public string Combine(string path1, string path2) => Path.Combine(path1, path2);
	public string? GetDirectoryName(string? path) => Path.GetDirectoryName(path);
	public string? GetFileName(string? path) => Path.GetFileName(path);
	public string? GetFileNameWithoutExtension(string? path) => Path.GetFileNameWithoutExtension(path);
	public string? GetExtension(string? path) => Path.GetExtension(path);
	public string Join(string? path1, string? path2) => Path.Join(path1, path2);
	public bool EndsInDirectorySeparator([NotNullWhen(true)] string? path) => Path.EndsInDirectorySeparator(path);
}
