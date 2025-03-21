using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RnCore.Sonarr.Commands;
using RnCore.Sonarr.Enums;
using RnCore.Sonarr.Resources;

namespace RnCore.Sonarr;

public interface ISonarrClient
{
	Task<SonarrManualImportResource[]> GetDownloadedSeriesFilesAsync(string? folder = null, CancellationToken cancellationToken = default);
	Task RefreshSeriesAsync(int seriesId, bool waitForCompletion, CancellationToken cancellationToken = default);
	Task ManualImportAsync(SonarrManualImportResource[] resources, bool waitForCompletion, CancellationToken cancellationToken = default);
}

public class SonarrClient : ISonarrClient
{
	private readonly HttpClient _httpClient = new();
	private readonly ILogger<SonarrClient> _logger;
	private readonly JsonSerializerOptions _jsonSerializerOptions;
	private readonly SonarrClientOptions _options;

	public SonarrClient(ILogger<SonarrClient> logger, SonarrClientOptions options)
	{
		_logger = logger;
		_httpClient.DefaultRequestHeaders.Add("X-Api-Key", options.ApiKey);
		_jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
		_options = options;

		ValidateOptions();
	}

	private void ValidateOptions()
	{
		if (!_options.DumpApiRequests) return;

		if (string.IsNullOrWhiteSpace(_options.ApiRequestDumpFilePath))
			throw new RnSonarrException("Option 'Sonarr.ApiRequestDumpPath' cannot be null when dumping is enabled");

		if (!Directory.Exists(_options.ApiRequestDumpFilePath))
			Directory.CreateDirectory(_options.ApiRequestDumpFilePath);

		if (!Directory.Exists(_options.ApiRequestDumpFilePath))
			throw new RnSonarrException($"Unable to create Sonarr API dump directory: {_options.ApiRequestDumpFilePath}");
	}


	// Public methods
	public async Task<SonarrManualImportResource[]> GetDownloadedSeriesFilesAsync(string? folder = null, CancellationToken cancellationToken = default)
	{
		// https://sonarr.tv/docs/api/#/ManualImport/get_api_v3_manualimport

		var urlBuilder = new SonarrUrlBuilder(_options.BaseUrl)
			.ForEndpoint(SonarrApiEndpoint.ManualImport)
			.WithQueryParameter("folder", string.IsNullOrWhiteSpace(folder) ? _options.DefaultDownloadDir : folder)
			.WithQueryParameter("filterExistingFiles", true);

		return await GetAsync<SonarrManualImportResource[]>(urlBuilder, cancellationToken);
	}

	public async Task RefreshSeriesAsync(int seriesId, bool waitForCompletion,
		CancellationToken cancellationToken = default)
	{
		var urlBuilder = new SonarrUrlBuilder(_options.BaseUrl)
			.ForEndpoint(SonarrApiEndpoint.Command);

		var command = new SonarrRefreshSeriesCommand(seriesId);
		var response = await PostAsync<SonarrCommandResource>(urlBuilder, command, cancellationToken);
		if (!waitForCompletion) return;
		await WaitForCommandToCompleteAsync(response.ID, cancellationToken);
	}

	public async Task ManualImportAsync(SonarrManualImportResource[] resources, bool waitForCompletion,
		CancellationToken cancellationToken = default)
	{
		var urlBuilder = new SonarrUrlBuilder(_options.BaseUrl)
			.ForEndpoint(SonarrApiEndpoint.Command);

		var response = await PostAsync<SonarrCommandResource>(urlBuilder,
			new SonarrManualImportCommand
			{
				Files = resources.Select(SonarrImportFileResource.FromManualImportResource).ToArray()
			},
			cancellationToken);

		if (!waitForCompletion) return;
		await WaitForCommandToCompleteAsync(response.ID, cancellationToken);
	}


	// Internal methods
	private async Task<TResult> PostAsync<TResult>(SonarrUrlBuilder urlBuilder, object postBody,
		CancellationToken cancellationToken)
	{
		var url = urlBuilder.Build();
		_logger.LogTrace("Sonarr API POST to {Url}", url);

		var postResponse = await _httpClient.SendAsync(
			new HttpRequestMessage(HttpMethod.Post, url)
			{
				Content = new StringContent(
					JsonSerializer.Serialize(postBody, _jsonSerializerOptions),
					Encoding.UTF8,
					"application/json"
				)
			},
			cancellationToken
		);

		postResponse.EnsureSuccessStatusCode();
		var responseJson = await postResponse.Content.ReadAsStringAsync(cancellationToken);
		var deserialize = JsonSerializer.Deserialize<TResult>(responseJson)!;

		DumpCommand(new SonarrApiRequestDumpInfo(url, HttpMethod.Post)
			.WithRequestObject(postBody)
			.WithResponseObject(deserialize));

		return deserialize;
	}

	private async Task<TResult> GetAsync<TResult>(SonarrUrlBuilder urlBuilder, CancellationToken cancellationToken)
	{
		var url = urlBuilder.Build();
		_logger.LogTrace("Sonarr API GET on {Url}", url);
		var request = new HttpRequestMessage(HttpMethod.Get, url);
		var response = await _httpClient.SendAsync(request, cancellationToken);
		var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
		var deserialize = JsonSerializer.Deserialize<TResult>(responseJson)!;
		DumpCommand(new SonarrApiRequestDumpInfo(url, HttpMethod.Get).WithResponseObject(deserialize));
		return deserialize;
	}

	private async Task WaitForCommandToCompleteAsync(int commandID, CancellationToken cancellationToken)
	{
		var completed = false;
		var urlBuilder = new SonarrUrlBuilder(_options.BaseUrl)
			.ForEndpoint(SonarrApiEndpoint.Command)
			.WithSegment(commandID);

		while (!completed)
		{
			await Task.Delay(250, cancellationToken);
			var response = await GetAsync<SonarrCommandResource>(urlBuilder, cancellationToken);
			completed = response.Status is SonarrCommandStatus.Completed or SonarrCommandStatus.Aborted
				or SonarrCommandStatus.Cancelled or SonarrCommandStatus.Failed;
			await Task.Delay(1750, cancellationToken);
		}
	}

	private void DumpCommand(SonarrApiRequestDumpInfo apiRequestInfo)
	{
		if (!_options.DumpApiRequests) return;
		var fileJson = JsonSerializer.Serialize(apiRequestInfo, _jsonSerializerOptions);
		File.WriteAllText(_options.GenerateDumpFilePath(), fileJson);
	}
}
