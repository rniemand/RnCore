using Microsoft.Extensions.Logging;

#pragma warning disable CA2254

namespace RnCore.Logging;

public interface ILoggerAdapter<out T> : ILoggerAdapter;

public class LoggerAdapter(ILogger logger) : ILoggerAdapter
{
	public void LogTrace(string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Trace)) return;
		logger.LogTrace(message, args);
	}

	public void LogTrace(Exception? exception, string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Trace)) return;
		logger.LogTrace(exception, message, args);
	}

	public void LogDebug(string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Debug)) return;
		logger.LogDebug(message, args);
	}

	public void LogDebug(Exception? exception, string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Debug)) return;
		logger.LogDebug(exception, message, args);
	}

	public void LogInformation(string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Information)) return;
		logger.LogInformation(message, args);
	}

	public void LogInformation(Exception? exception, string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Information)) return;
		logger.LogInformation(exception, message, args);
	}

	public void LogWarning(string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Warning)) return;
		logger.LogWarning(message, args);
	}

	public void LogWarning(Exception? exception, string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Warning)) return;
		logger.LogWarning(exception, message, args);
	}

	public void LogError(string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Error)) return;
		logger.LogError(message, args);
	}

	public void LogError(Exception? exception, string? message, params object?[] args)
	{
		if (!logger.IsEnabled(LogLevel.Error)) return;
		logger.LogError(exception, message, args);
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
		Func<TState, Exception?, string> formatter) =>
		logger.Log(logLevel, eventId, state, exception, formatter);

	public bool IsEnabled(LogLevel logLevel) =>
		logger.IsEnabled(logLevel);

	public IDisposable? BeginScope<TState>(TState state) where TState : notnull =>
		logger.BeginScope(state);
}

public class LoggerAdapter<T>(ILogger<T> logger) : LoggerAdapter(logger), ILoggerAdapter<T>;
