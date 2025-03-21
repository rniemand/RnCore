using System.Diagnostics;

namespace RnCore.Metrics;

public interface IMetricTimingToken : IDisposable { }

public class MetricTimingToken<TBuilder>(IBaseMetricBuilder<TBuilder> builder, string fieldName) : IMetricTimingToken
{
	private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

	public void Dispose()
	{
		var elapsedMs = _stopwatch.ElapsedMilliseconds;
		builder.AddAction(m => { m.SetField(fieldName, elapsedMs); });
		GC.SuppressFinalize(this);
	}
}
