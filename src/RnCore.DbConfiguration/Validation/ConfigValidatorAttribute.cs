namespace RnCore.DbConfiguration.Validation;

[AttributeUsage(AttributeTargets.Property)]
public abstract class ConfigValidatorAttribute(ConfigValidator validator) : Attribute
{
	public ConfigValidator Validator { get; } = validator;

	public virtual bool Validate(string propName, object? rawValue, ValidationOutcome outcome) =>
		throw new NotImplementedException();
}
