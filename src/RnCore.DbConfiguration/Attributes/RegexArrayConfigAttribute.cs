using System.Reflection;
using System.Text.RegularExpressions;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RegexArrayConfigAttribute(string propertyName, RegexOptions rxOptions = RegexOptions.Compiled) : NlpDbConfigAttribute(propertyName, DbValueType.RegexArray)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasStringCollection(PropertyName))
			return false;

		var rawEntries = options.GetRegexCollection(PropertyName, rxOptions).ToArray();
		propertyInfo.SetValue(obj, rawEntries);

		return true;
	}
}
