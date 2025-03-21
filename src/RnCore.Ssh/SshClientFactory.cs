using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

namespace RnCore.Ssh;

public interface ISshClientFactory
{
	Task<ISshClientWrapper> GetSshClient(string credentialsName, CancellationToken cancellationToken);
}

public class SshClientFactory(
	ISshCredentialsProvider credentialsProvider,
	IServiceProvider serviceProvider
) : ISshClientFactory
{
	public async Task<ISshClientWrapper> GetSshClient(string credentialsName, CancellationToken cancellationToken) => new SshClientWrapper(
		serviceProvider.GetRequiredService<ILoggerAdapter<SshClientWrapper>>(),
		await credentialsProvider.GetSshCredentialsAsync(credentialsName, cancellationToken),
		credentialsName
	);
}
