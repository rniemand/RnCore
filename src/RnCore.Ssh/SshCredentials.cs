using System.Text.Json.Serialization;

namespace RnCore.Ssh;

public class SshCredentials
{
	[JsonPropertyName("host")]
	public string Host { get; set; } = string.Empty;

	[JsonPropertyName("port")]
	public int Port { get; set; } = 22;

	[JsonPropertyName("user")]
	public string User { get; set; } = string.Empty;

	[JsonPropertyName("pass")]
	public string Pass { get; set; } = string.Empty;

	public bool IsValid()
	{
		if (string.IsNullOrWhiteSpace(Host))
			return false;

		if (string.IsNullOrWhiteSpace(User))
			return false;

		if (string.IsNullOrWhiteSpace(Pass))
			return false;

		return true;
	}
}
