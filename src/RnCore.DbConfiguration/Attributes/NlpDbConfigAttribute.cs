using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

// TODO: (RENAME) Rename this once we have sufficient test coverage
[AttributeUsage(AttributeTargets.Property)]
public abstract class NlpDbConfigAttribute(string propertyName, string type) : Attribute
{
	public string PropertyName { get; } = propertyName;

	// TODO: [REMOVE] (NlpDbConfigAttribute.ConfigType) This is not being used and can be removed!
	public string ConfigType { get; } = type;

	public virtual bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options) =>
		throw new NotImplementedException();
}
