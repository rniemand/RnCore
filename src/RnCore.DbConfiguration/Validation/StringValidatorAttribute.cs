namespace RnCore.DbConfiguration.Validation;

public class StringValidatorAttribute() : ConfigValidatorAttribute(ConfigValidator.String)
{
	// https://source.dot.net/#System.Configuration.ConfigurationManager/artifacts/obj/System.Configuration.ConfigurationManager/Debug/net10.0/System.SR.cs,1986d7b73b1ef7a8

	private int _maxLength = int.MaxValue;
	private int _minLength;

	public override bool Validate(string propName, object? rawValue, ValidationOutcome outcome)
	{
		if (rawValue is not string strValue)
		{
			outcome.WithError($"'{propName}' should be a STRING value");
			return false;
		}

		if (string.IsNullOrWhiteSpace(strValue) && Required)
		{
			outcome.WithError($"'{propName}' is required and needs to be set");
			return false;
		}

		var strLength = strValue.Length;

		if (_minLength > 0 && strLength < _minLength)
		{
			outcome.WithError($"'{propName}' needs to be longer than {_minLength}");
			return false;
		}

		if (strLength > _maxLength)
		{
			outcome.WithError($"'{propName}' cannot exceed {_maxLength} length");
			return false;
		}

		return true;
	}

	public int MinLength
	{
		get => _minLength;
		set
		{
			if (_maxLength < value)
				throw new ArgumentOutOfRangeException(nameof(value), "The upper range limit value must be greater than the lower range limit value.");

			_minLength = value;
		}
	}

	public int MaxLength
	{
		get => _maxLength;
		set
		{
			if (_minLength > value)
				throw new ArgumentOutOfRangeException(nameof(value), "The upper range limit value must be greater than the lower range limit value.");

			_maxLength = value;
		}
	}

	public bool Required { get; set; } = false;
}
