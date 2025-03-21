using System.Text.Json.Serialization;

namespace RnCore.Logging;

public class RnLogFileEntry
{
	[JsonPropertyName("hostname")]
	public string Hostname { get; set; } = string.Empty;

	[JsonPropertyName("applicationName"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public string ApplicationName { get; set; } = string.Empty;

	[JsonPropertyName("entryDateTime")]
	public DateTime EntryDateTime { get; set; } = DateTime.MinValue;

	[JsonPropertyName("severity")]
	public string Severity { get; set; } = SeverityMapper.Unknown;

	[JsonPropertyName("eventID"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public int EventID { get; set; }

	[JsonPropertyName("logger"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public string Logger { get; set; } = string.Empty;

	[JsonPropertyName("lineNumber"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public int LineNumber { get; set; } = 0;

	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;

	[JsonPropertyName("exName"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public string ExceptionName { get; set; } = string.Empty;

	[JsonPropertyName("exMessage"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public string ExceptionMessage { get; set; } = string.Empty;

	[JsonPropertyName("stackTrace"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public string StackTrace { get; set; } = string.Empty;
}
