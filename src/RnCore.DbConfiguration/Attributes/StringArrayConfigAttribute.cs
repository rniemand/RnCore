using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class StringArrayConfigAttribute(string propertyName) : NlpDbConfigAttribute(propertyName, DbValueType.StringArray)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasStringCollection(PropertyName))
			return false;

		propertyInfo.SetValue(obj, options.GetStringCollection(PropertyName).ToArray());
		return true;
	}
}
