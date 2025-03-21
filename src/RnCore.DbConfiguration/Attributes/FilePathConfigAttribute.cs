using System.Reflection;

namespace RnCore.DbConfiguration.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FilePathConfigAttribute(string propertyName) : NlpDbConfigAttribute(propertyName, DbValueType.String)
{
	public override bool TrySetValue(PropertyInfo propertyInfo, object? obj, INlpDbConfigCollection options)
	{
		if (!options.HasFilePathValue(PropertyName))
			return false;

		propertyInfo.SetValue(obj, options.GetFilePathValue(PropertyName));
		return true;
	}
}
