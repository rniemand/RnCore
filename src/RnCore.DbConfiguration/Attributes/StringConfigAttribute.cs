using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class StringConfigAttribute(string propertyName) : NlpDbConfigAttribute(propertyName, DbValueType.String)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasStringValue(PropertyName))
			return false;

		propertyInfo.SetValue(obj, options.GetStringValue(PropertyName));
		return true;
	}
}
