namespace RnCore.Ssh;

public interface ISshCredentialsProvider
{
	Task<SshCredentials> GetSshCredentialsAsync(string credentialsName, CancellationToken cancellationToken);
}
