namespace RnCore.Logging;

public class SeverityMapper
{
	public const string Unknown = "UNKNOWN";
	public const string Debug = "DEBUG";
	public const string Info = "INFO";
	public const string Trace = "TRACE";
	public const string Warning = "WARNING";
	public const string Error = "ERROR";
	public const string Fatal = "FATAL";

	public static string Map(string value)
	{
		if (string.IsNullOrWhiteSpace(value)) return Unknown;
		var safeValue = value.ToLower().Trim();

		if (safeValue == "debug") return Debug;
		if (safeValue == "info") return Info;
		if (safeValue == "trace") return Trace;
		if (safeValue == "warn") return Warning;
		if (safeValue == "error") return Error;
		if (safeValue == "fatal") return Fatal;

		throw new RnLogsException($"Unable to map severity: {value}");
	}
}
