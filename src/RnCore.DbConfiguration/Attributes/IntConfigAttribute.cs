using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IntConfigAttribute(string propertyName) : NlpDbConfigAttribute(propertyName, DbValueType.String)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasIntValue(PropertyName))
			return false;

		propertyInfo.SetValue(obj, options.GetIntValue(PropertyName));
		return true;
	}
}
