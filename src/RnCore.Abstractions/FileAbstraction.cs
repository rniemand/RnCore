using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RnCore.Abstractions;

public interface IFileAbstraction
{
	bool Exists([NotNullWhen(true)] string? path);
	string ReadAllText(string path);
	void Delete(string path);
	void Move(string sourceFileName, string destFileName);
	void WriteAllText(string path, string? contents, Encoding encoding);
}

public class FileAbstraction : IFileAbstraction
{
	public bool Exists([NotNullWhen(true)] string? path) => File.Exists(path);
	public string ReadAllText(string path) => File.ReadAllText(path);
	public void Delete(string path) => File.Delete(path);
	public void Move(string sourceFileName, string destFileName) => File.Move(sourceFileName, destFileName);
	public void WriteAllText(string path, string? contents, Encoding encoding) => File.WriteAllText(path, contents, encoding);
}
