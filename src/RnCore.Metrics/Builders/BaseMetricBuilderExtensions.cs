namespace RnCore.Metrics;

public static class BaseMetricBuilderExtensions
{
	public static IMetricTimingToken WithTiming<TBuilder>(this TBuilder builder, string? field = null)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		if (string.IsNullOrWhiteSpace(field))
			field = "value";

		return new MetricTimingToken<TBuilder>(builder, field);
	}

	public static TBuilder WithTag<TBuilder>(this TBuilder builder, string tag, string value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetTag(tag, value); });
		return builder;
	}

	public static TBuilder WithSuccess<TBuilder>(this TBuilder builder, bool success)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.SetSuccess(success);
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, int value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, long value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, double value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, float value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, bool value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, byte value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, short value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, decimal value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}

	public static TBuilder WithField<TBuilder>(this TBuilder builder, string field, TimeSpan value)
		where TBuilder : IBaseMetricBuilder<TBuilder>
	{
		builder.AddAction(m => { m.SetField(field, value); });
		return builder;
	}
}
