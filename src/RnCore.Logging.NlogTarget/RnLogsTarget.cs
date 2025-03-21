using System.Text;
using System.Text.Json;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace RnCore.Logging.NlogTarget;

[Target("RnCore.NLogTarget")]
public class RnLogsTarget : TargetWithLayout
{
	// https://nlog-project.org/2015/06/30/extending-nlog-is-easy.html

	[RequiredParameter]
	public string IngestionUrl { get; set; } = null!;

	[RequiredParameter]
	public string Host { get; set; } = null!;

	[RequiredParameter]
	public string Application { get; set; } = null!;

	public int MaxSendErrorCount { get; set; } = 5;

	public int BackoffTimeMs { get; set; } = 5000;

	// Internal properties
	private bool _destinationEnabled = true;
	private int _sendErrorCount;
	private readonly HttpClient _httpClient = new();
	private DateTime? _backoffTimeEnds;
	private readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		WriteIndented = true
	};

	// Interface methods
	protected override void Write(LogEventInfo logEvent)
	{
		SendTheMessageToRemoteHost(logEvent);
	}

	// Internal methods
	private void SendTheMessageToRemoteHost(LogEventInfo logEvent)
	{
		SubmitLogMessage(GenerateEntry(logEvent));
	}

	private RnLogFileEntry GenerateEntry(LogEventInfo logEvent) => new()
	{
		Message = Layout.Render(logEvent),
		Logger = logEvent.LoggerName ?? "(unknown)",
		EventID = ExtractEventID(logEvent),
		EntryDateTime = logEvent.TimeStamp,
		Hostname = Host,
		LineNumber = logEvent.CallerLineNumber,
		Severity = SeverityMapper.Map(logEvent.Level.Name),
		ExceptionName = logEvent.Exception?.GetType().Name ?? string.Empty,
		ExceptionMessage = logEvent.Exception?.Message ?? string.Empty,
		StackTrace = logEvent.HasStackTrace ? logEvent.StackTrace.ToString() : string.Empty,
		ApplicationName = Application
	};

	private static int ExtractEventID(LogEventInfo logEvent)
	{
		if (!logEvent.HasProperties) return 0;

		if (logEvent.Properties.TryGetValue("EventId", out var property))
			return (int)property;

		return 0;
	}

	private void SubmitLogMessage(RnLogFileEntry entry)
	{
		if (!_destinationEnabled) return;
		if (_backoffTimeEnds.HasValue)
		{
			if (DateTime.Now < _backoffTimeEnds.Value) return;
			_backoffTimeEnds = null;
		}

		try
		{
			var rawJson = JsonSerializer.Serialize(entry, _jsonSerializerOptions);
			var requestMessage = new HttpRequestMessage(HttpMethod.Post, IngestionUrl);
			requestMessage.Content = new StringContent(rawJson, Encoding.UTF8, "application/json");
			_httpClient.Send(requestMessage);
			_sendErrorCount = 0;
		}
		catch
		{
			_backoffTimeEnds = DateTime.Now.AddMilliseconds(BackoffTimeMs);
			_sendErrorCount++;
			_destinationEnabled = _sendErrorCount > MaxSendErrorCount;
		}
	}
}
