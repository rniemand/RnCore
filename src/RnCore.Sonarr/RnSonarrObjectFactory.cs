using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RnCore.Sonarr;

public interface IRnSonarrObjectFactory
{
	ISonarrClient CreateSonarrClient(ILogger<SonarrClient> logger, SonarrClientOptions options);
	ISonarrClient CreateSonarrClient(SonarrClientOptions options);
}

public class RnSonarrObjectFactory(IServiceProvider serviceProvider) : IRnSonarrObjectFactory
{
	public ISonarrClient CreateSonarrClient(ILogger<SonarrClient> logger, SonarrClientOptions options) =>
		new SonarrClient(logger, options);

	public ISonarrClient CreateSonarrClient(SonarrClientOptions options) => CreateSonarrClient(
		serviceProvider.GetRequiredService<ILogger<SonarrClient>>(),
		options
	);
}
