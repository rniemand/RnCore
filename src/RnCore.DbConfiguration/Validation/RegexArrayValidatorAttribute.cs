using System.Text.RegularExpressions;

namespace RnCore.DbConfiguration.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class RegexArrayValidatorAttribute() : ConfigValidatorAttribute(ConfigValidator.StringArray)
{
	public int MinLength
	{
		get => _minLength;
		set
		{
			_minLengthSet = true;
			_minLength = value;
		}
	}

	private bool _minLengthSet;
	private int _minLength;

	public RegexArrayValidatorAttribute(int minLength)
		: this()
	{
		MinLength = minLength;
	}

	public override bool Validate(string propName, object? rawValue, ValidationOutcome outcome)
	{
		if (rawValue is null)
		{
			outcome.WithError($"'{propName}' is required and cannot be NULL");
			return false;
		}

		if (rawValue is not Regex[] strArray)
		{
			var propType = rawValue.GetType().Name;
			outcome.WithError($"'{propName}' is of type '{propType}' and cannot be validated as a Regex[]");
			return false;
		}

		if (_minLengthSet && strArray.Length < MinLength)
		{
			outcome.WithError($"'{propName}' needs at lease {MinLength} value(s) - has {strArray.Length}");
			return false;
		}

		return true;
	}
}
