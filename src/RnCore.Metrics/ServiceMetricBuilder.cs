namespace RnCore.Metrics;

public class ServiceMetricBuilder() : BaseMetricBuilder<ServiceMetricBuilder>(Measurement)
{
	public const string Measurement = "service_call";
	private string _serviceName = string.Empty;
	private string _methodName = string.Empty;

	public ServiceMetricBuilder(string serviceName, string methodName)
		: this()
	{
		ForService(serviceName).ForMethod(methodName);
	}

	public ServiceMetricBuilder ForService(string service)
	{
		_serviceName = service;
		return this;
	}

	public ServiceMetricBuilder ForMethod(string method)
	{
		_methodName = method;
		return this;
	}

	public ServiceMetricBuilder WithException(Exception ex)
	{
		SetException(ex);
		return this;
	}

	public override RnMetric Build()
	{
		// Set repo specific Fields and Tags
		AddAction(m => m.SetTag("service_name", _serviceName))
			.AddAction(m => m.SetTag("service_method", _methodName))
			.AddAction(m => m.SetField("call_count", 1));

		// Bake in all other Fields and Tags
		return base.Build();
	}
}
