using System.Net;
using System.Text.RegularExpressions;
using RnCore.Abstractions;
using RnCore.DbConfiguration;
using RnCore.Logging;
using RnCore.Mailer.Builders;

namespace RnCore.Mailer;

public interface IRnMailerFactory
{
	ISmtpClient CreateSmtpClient();
	RnMailConfig GetMailConfig();
	IMailTemplateBuilder GetTemplateBuilder(RnMailConfig mailConfig, string templateName);
}

public class RnMailerFactory(
	ILoggerAdapter<RnMailerFactory> logger,
	IDbConfigurationService dbConfigurationService,
	IFileAbstraction file,
	IPathAbstraction path
) : IRnMailerFactory
{
	private RnMailConfig? _mailConfig;
	private string _templateDir = string.Empty;
	private string _cssDir = string.Empty;


	// Public methods
	public ISmtpClient CreateSmtpClient()
	{
		var rnMailConfig = GetMailConfig();
		var smtpClient = new SmtpClientWrapper(rnMailConfig.Host, rnMailConfig.Port)
		{
			DeliveryFormat = rnMailConfig.DeliveryFormat,
			DeliveryMethod = rnMailConfig.DeliveryMethod,
			EnableSsl = rnMailConfig.EnableSsl,
			PickupDirectoryLocation = null,
			TargetName = null,
			Timeout = rnMailConfig.Timeout,
			UseDefaultCredentials = false
		};

		if (rnMailConfig.HasCredentials())
			smtpClient.Credentials = new NetworkCredential(rnMailConfig.Username, rnMailConfig.Password);

		logger.LogDebug("Created new instance for: {host}", rnMailConfig.Host);
		return smtpClient;
	}

	public RnMailConfig GetMailConfig()
	{
		EnsureConfigured();

		if (_mailConfig is null)
			throw new RnMailerException("Unable to generate mail configuration");

		return _mailConfig;
	}

	public string GetTemplate(RnMailConfig mailConfig, string name)
	{
		EnsurePathsDefined(mailConfig);
		var tplFilePath = GenerateTemplatePath(name);

		if (file.Exists(tplFilePath))
			return file.ReadAllText(tplFilePath);

		logger.LogError("Unable to resolve template file path: {path}", tplFilePath);
		return string.Empty;
	}

	public string GetCss(RnMailConfig mailConfig, string name)
	{
		EnsurePathsDefined(mailConfig);
		var filePath = GenerateCssPath(name);

		if (file.Exists(filePath))
			return file.ReadAllText(filePath);

		logger.LogWarning("Unable to find requested CSS file: {path}", filePath);
		return string.Empty;
	}

	public IMailTemplateBuilder GetTemplateBuilder(RnMailConfig mailConfig, string templateName)
	{
		logger.LogDebug("Resolving template: {name}", templateName);
		var templateBuilder = new MailTemplateBuilder
		{
			RawTemplate = GetTemplate(mailConfig, templateName),
			TemplateName = templateName
		};

		if (!templateBuilder.TemplateFound)
			return templateBuilder;

		ProcessCssTags(mailConfig, templateBuilder);
		return templateBuilder;
	}


	// Internal methods
	private void EnsureConfigured()
	{
		if (_mailConfig is not null) return;
		_mailConfig = dbConfigurationService.BindConfiguration<RnMailConfig>("RnMailConfig");
	}

	private void EnsurePathsDefined(RnMailConfig mailConfig)
	{
		if (string.IsNullOrWhiteSpace(_templateDir))
			_templateDir = GenerateTemplateDirPath(mailConfig);

		if (string.IsNullOrWhiteSpace(_cssDir))
			_cssDir = GenerateCssDirPath(_templateDir);
	}

	private string GenerateTemplateDirPath(RnMailConfig mailConfig)
	{
		var templateDir = mailConfig.TemplateDir;

		if (templateDir.StartsWith("./"))
			templateDir = path.Join(Environment.CurrentDirectory, templateDir[2..]);

		// ReSharper disable once ConvertIfStatementToReturnStatement
		if (!Path.EndsInDirectorySeparator(templateDir))
			return path.Join(templateDir, Path.DirectorySeparatorChar.ToString());

		return templateDir;
	}

	private string GenerateCssDirPath(string templateDir)
	{
		var basePath = path.Join(templateDir, "css");

		// ReSharper disable once ConvertIfStatementToReturnStatement
		if (!path.EndsInDirectorySeparator(basePath))
			return path.Join(basePath, Path.DirectorySeparatorChar.ToString());

		return basePath;
	}

	private string GenerateTemplatePath(string name) => path.Join(_templateDir, $"{name}.html");

	private string GenerateCssPath(string name) => path.Join(_cssDir, $"{name}.css");

	private void ProcessCssTags(RnMailConfig mailConfig, MailTemplateBuilder builder)
	{
		// (\{css:([^\}]+)\})
		const string regex = "(\\{css:([^\\}]+)\\})";
		if (!Regex.IsMatch(builder.RawTemplate, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline))
			return;

		var matches = Regex.Matches(builder.RawTemplate, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
		foreach (var groups in matches.Select(x => x.Groups))
			builder.ReplaceCssTag(groups[1].Value, GetCss(mailConfig, groups[2].Value));
	}
}
