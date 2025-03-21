using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class BoolConfigAttribute(string propertyName, bool fallback) : NlpDbConfigAttribute(propertyName, DbValueType.String)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasBoolValue(PropertyName))
			return false;

		propertyInfo.SetValue(obj, options.GetBoolValue(PropertyName, fallback));
		return true;
	}
}
