using RnCore.DbConfiguration.Attributes;
using RnCore.DbConfiguration.Validation;
using RnCore.Logging;

namespace RnCore.DbConfiguration;

public interface IDbConfigurationService
{
	TClass BindConfiguration<TClass>(string configCategory) where TClass : class, new();
	Task SaveConfigEntryAsync(ConfigurationEntity entry);
	Task<IEnumerable<ConfigurationEntity>> GetConfigEntriesAsync(string host, string category, CancellationToken cancellationToken);
}

public class DbConfigurationService(
	ILoggerAdapter<DbConfigurationService> logger,
	IConfigurationRepo configurationRepo,
	string hostName
) : IDbConfigurationService
{
	private readonly List<ConfigurationEntity> _dbConfiguration = [];
	private DateTime _nextConfigRefreshTime = DateTime.MinValue;

	// ------------------------------------------------------------ >>
	// Interface methods
	public TClass BindConfiguration<TClass>(string configCategory) where TClass : class, new()
	{
		EnsureConfigurationUpToDate();
		var options = new NlpDbConfigCollection(GetConfigCategoryEntries(configCategory));
		return (TClass)BindConfigurationRecursive(typeof(TClass), options);
	}

	public async Task SaveConfigEntryAsync(ConfigurationEntity entry)
	{
		if (string.IsNullOrWhiteSpace(entry.HostName)) entry.HostName = hostName;

		var collectionEntry = _dbConfiguration.FirstOrDefault(x =>
			x.IsCollection == entry.IsCollection &&
			x.HostName == entry.HostName &&
			x.Category == entry.Category &&
			x.ConfigKey == entry.ConfigKey &&
			x.ConfigType == entry.ConfigType
		);

		// Add new entry
		if (collectionEntry is null)
		{
			_dbConfiguration.Add(entry);
			await configurationRepo.InsertEntryAsync(entry);
			return;
		}

		// Update existing entry
		collectionEntry.ConfigValue = entry.ConfigValue;
		await configurationRepo.UpdateEntryAsync(entry);
	}

	public Task<IEnumerable<ConfigurationEntity>> GetConfigEntriesAsync(string host, string category, CancellationToken cancellationToken) =>
		configurationRepo.GetAllConfigurationAsync(hostName, category, cancellationToken);

	// ------------------------------------------------------------ >>
	// Internal methods
	private object BindConfigurationRecursive(Type tClass, NlpDbConfigCollection options)
	{
		var dbConfigAttributeType = typeof(NlpDbConfigAttribute);
		var mappableClassType = typeof(DbConfigurationMappableClass);
		var classProperties = tClass.GetProperties();

		var objInstance = Activator.CreateInstance(tClass);
		if (objInstance is null)
			throw new RnDbConfigurationException($"Failed to create instance of: {tClass.FullName}");

		var propertyInfos = classProperties.Where(p =>
			p.CustomAttributes.Any(a => a.AttributeType.IsAssignableTo(dbConfigAttributeType)) ||
			(p.PropertyType.IsAssignableTo(mappableClassType) && p.CanWrite)
		).ToList();

		// Discover and bind all class properties recursive
		foreach (var propertyInfo in propertyInfos)
		{
			if (propertyInfo.PropertyType.IsAssignableTo(mappableClassType))
			{
				propertyInfo.SetValue(objInstance, BindConfigurationRecursive(propertyInfo.PropertyType, options));
				continue;
			}

			if (Attribute.GetCustomAttribute(propertyInfo, dbConfigAttributeType) is not NlpDbConfigAttribute attribute)
				continue;

			if (attribute.TrySetValue(propertyInfo, objInstance, options))
				continue;

			logger.LogWarning(
				"Unable to map property '{property}' on {class} - using default value",
				propertyInfo.Name,
				tClass.FullName
			);
		}

		// Run property validation
		var validationOutcome = ValidateConfig(objInstance);
		if (!validationOutcome.Success)
			throw new RnDbConfigurationException(validationOutcome.ValidationError);

		return objInstance;
	}

	private void EnsureConfigurationUpToDate()
	{
		// Reload configuration periodically
		if (_dbConfiguration.Count > 0 && DateTime.Now < _nextConfigRefreshTime)
			return;

		logger.LogInformation("Refreshing configuration from DB");

		_dbConfiguration.Clear();
		_dbConfiguration.AddRange(configurationRepo
			.GetAllConfigurationAsync(hostName)
			.ConfigureAwait(false)
			.GetAwaiter()
			.GetResult());
		_nextConfigRefreshTime = DateTime.Now.AddMinutes(5);
	}

	private List<ConfigurationEntity> GetConfigCategoryEntries(string category) => _dbConfiguration
		.Where(x =>
			x.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase) &&
			(x.HostName.Equals(hostName, StringComparison.InvariantCultureIgnoreCase) || x.HostName == "*")
		).ToList();

	private static ValidationOutcome ValidateConfig(object mappedObject)
	{
		var validationOutcome = new ValidationOutcome();
		var validatorAttributeType = typeof(ConfigValidatorAttribute);

		var propertyInfos = mappedObject.GetType()
			.GetProperties()
			.Where(p => p.CustomAttributes.Any(a => a.AttributeType.IsAssignableTo(validatorAttributeType)))
			.ToList();

		foreach (var propertyInfo in propertyInfos)
		{
			if (Attribute.GetCustomAttribute(propertyInfo, validatorAttributeType) is not ConfigValidatorAttribute attribute)
				continue;

			if (!attribute.Validate(propertyInfo.Name, propertyInfo.GetValue(mappedObject), validationOutcome))
				return validationOutcome;
		}

		return validationOutcome;
	}
}
