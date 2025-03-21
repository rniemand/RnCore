using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DateConfigAttribute(string propertyName) : NlpDbConfigAttribute(propertyName, DbValueType.DateTime)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasDateTimeValue(PropertyName))
			return false;

		propertyInfo.SetValue(obj, options.GetDateTimeValue(PropertyName, DateTime.MinValue));
		return true;
	}
}
