using NasHome.Common.Exceptions;
using Renci.SshNet;
using RnCore.Logging;

namespace RnCore.Ssh;

public interface ISshClientWrapper
{
	void RunCommand(string commandText, bool throwOnError = true);
}

public class SshClientWrapper : ISshClientWrapper
{
	private readonly ILoggerAdapter<SshClientWrapper> _logger;
	private readonly SshClient? _client;

	public SshClientWrapper(ILoggerAdapter<SshClientWrapper> logger,
		SshCredentials credentials,
		string credentialsName)
	{
		_logger = logger;

		if (!credentials.IsValid())
		{
			_logger.LogError("Invalid credentials provided for '{name}'", credentialsName);
			return;
		}

		_client = new SshClient(new ConnectionInfo(
			credentials.Host,
			credentials.Port,
			credentials.User,
			new PasswordAuthenticationMethod(credentials.User, credentials.Pass))
		);

		_client.Connect();
	}


	// Interface methods
	public void RunCommand(string commandText, bool throwOnError = true)
	{
		if (_client is null || !_client.IsConnected)
			return;

		_logger.LogDebug("Running SSH command: {cmd}", commandText);
		var commandOutput = _client.RunCommand(commandText);

		if (string.IsNullOrWhiteSpace(commandOutput.Error) || !throwOnError)
			return;

		_logger.LogError("Error running command '{cmd}': {error}", commandOutput, commandOutput.Error);
		throw new RnCoreSshException($"ssh command error: {commandOutput.Error}");
	}
}
