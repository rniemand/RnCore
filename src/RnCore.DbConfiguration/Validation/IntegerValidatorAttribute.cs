namespace RnCore.DbConfiguration.Validation;

public class IntegerValidatorAttribute() : ConfigValidatorAttribute(ConfigValidator.Int)
{
	// https://source.dot.net/#System.Configuration.ConfigurationManager/artifacts/obj/System.Configuration.ConfigurationManager/Debug/net10.0/System.SR.cs,1986d7b73b1ef7a8

	private int _max = int.MaxValue;
	private int _min = int.MinValue;

	public override bool Validate(string propName, object? rawValue, ValidationOutcome outcome)
	{
		if (rawValue is not int intValue)
		{
			outcome.WithError($"'{propName}' should be a INT value");
			return false;
		}

		if (intValue < MinValue)
		{
			outcome.WithError($"'{propName}' is lower than the minimum allowed value of {_min}");
			return false;
		}

		if (intValue > MaxValue)
		{
			outcome.WithError($"'{propName}' is greater than the minimum allowed value of {_max}");
			return false;
		}

		return true;
	}

	public int MinValue
	{
		get => _min;
		set
		{
			if (_max < value)
				throw new ArgumentOutOfRangeException(nameof(value), "The upper range limit value must be greater than the lower range limit value.");
			_min = value;
		}
	}

	public int MaxValue
	{
		get => _max;
		set
		{
			if (_min > value)
				throw new ArgumentOutOfRangeException(nameof(value), "The upper range limit value must be greater than the lower range limit value.");
			_max = value;
		}
	}

	public bool ExcludeRange { get; set; }
}
