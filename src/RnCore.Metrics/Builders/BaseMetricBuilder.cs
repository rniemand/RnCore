namespace RnCore.Metrics;

public interface IBaseMetricBuilder<TBuilder>
{
	IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action);
	void SetSuccess(bool success);
	RnMetric Build();
}

public abstract class BaseMetricBuilder<TBuilder>(string measurement) : IBaseMetricBuilder<TBuilder>
{
	private readonly List<Action<RnMetric>> _actions = [];

	private bool _success = true;
	private bool _hasException;
	private string _exName = string.Empty;

	public IBaseMetricBuilder<TBuilder> AddAction(Action<RnMetric> action)
	{
		_actions.Add(action);
		return this;
	}

	public void SetSuccess(bool success)
	{
		_success = success;
	}

	protected void SetException(Exception ex) =>
		SetException(ex.GetType().Name);

	protected void SetException(string exceptionName)
	{
		SetSuccess(false);
		_hasException = true;
		_exName = exceptionName;
	}

	public virtual RnMetric Build()
	{
		// Ensure that core fields and tags exist
		AddAction(m => { m.SetTag("success", _success); })
			.AddAction(m => { m.SetTag("has_ex", _hasException); })
			.AddAction(m => { m.SetTag("ex_name", _exName); });

		// Compile and build the metric
		var metric = new RnMetric(measurement);
		_actions.ForEach(a => a(metric));
		return metric;
	}
}
