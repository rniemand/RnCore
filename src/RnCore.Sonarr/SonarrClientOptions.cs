namespace RnCore.Sonarr;

public class SonarrClientOptions
{
	public required string BaseUrl { get; set; }
	public required string ApiKey { get; set; }
	public bool DumpApiRequests { get; set; } = false;
	public string? ApiRequestDumpFilePath { get; set; }
	public string DefaultDownloadDir { get; set; } = "/data/tv/";

	private readonly Random _random = new(DateTime.Now.Millisecond);

	public string GenerateDumpFilePath()
	{
		var currentDate = DateTime.Now.ToString("s").Replace(":", "").Replace("T", "");
		var uid = _random.Next(10000, 99999);
		var fileName = $"{currentDate}.{uid}.json";
		return Path.Combine(ApiRequestDumpFilePath!, fileName);
	}
}
