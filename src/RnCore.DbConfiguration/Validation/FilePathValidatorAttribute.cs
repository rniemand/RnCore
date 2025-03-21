namespace RnCore.DbConfiguration.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class FilePathValidatorAttribute() : ConfigValidatorAttribute(ConfigValidator.FilePath)
{
	public bool Required { get; set; } = false;

	public override bool Validate(string propName, object? rawValue, ValidationOutcome outcome)
	{
		if (rawValue is null && Required)
		{
			outcome.WithError($"'{propName}' is required and cannot be NULL");
			return false;
		}

		if (rawValue is not string strValue)
		{
			outcome.WithError($"'{propName}' should be a STRING value");
			return false;
		}

		if (string.IsNullOrWhiteSpace(strValue) && Required)
		{
			outcome.WithError($"'{propName}' required and must have a value");
			return false;
		}

		var fileExists = File.Exists(strValue);
		var directoryExists = Directory.Exists(strValue);

		if (!fileExists && !directoryExists && Required)
		{
			outcome.WithError($"'{propName}' does not refer to a valid FILE or DIRECTORY");
			return false;
		}

		return true;
	}
}
