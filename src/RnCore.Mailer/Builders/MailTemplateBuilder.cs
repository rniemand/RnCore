using System.Text.RegularExpressions;

namespace RnCore.Mailer.Builders;

public interface IMailTemplateBuilder
{
	bool TemplateFound { get; }
	string RawTemplate { get; set; }
	string TemplateName { get; set; }
	Dictionary<string, object> Placeholders { get; set; }
	IMailTemplateBuilder AddPlaceHolder(string key, object value);
	IMailTemplateBuilder AddPlaceholders(Dictionary<string, object> placeholders);
	IMailTemplateBuilder ReplaceCssTag(string placeholder, string css);
	string Process();
}

public class MailTemplateBuilder : IMailTemplateBuilder
{
	public bool TemplateFound => !string.IsNullOrWhiteSpace(RawTemplate);
	public string RawTemplate { get; set; } = string.Empty;
	public string TemplateName { get; set; } = string.Empty;
	public Dictionary<string, object> Placeholders { get; set; } = new();

	public IMailTemplateBuilder AddPlaceHolder(string key, object value)
	{
		Placeholders[key] = value;
		return this;
	}

	public IMailTemplateBuilder AddPlaceholders(Dictionary<string, object> placeholders)
	{
		foreach (var placeholder in placeholders)
		{
			Placeholders[placeholder.Key] = placeholder.Value;
		}

		return this;
	}

	public IMailTemplateBuilder ReplaceCssTag(string placeholder, string css)
	{
		RawTemplate = RawTemplate.Replace(placeholder, $"<style>{css}</style>");
		return this;
	}

	public string Process()
	{
		var processed = RawTemplate;
		const string regex = "(\\{\\{([^\\}]+)\\}\\})";
		if (!Regex.IsMatch(processed, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline))
			return processed;

		var matches = Regex.Matches(processed, regex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
		foreach (Match match in matches)
		{
			var placeholder = match.Groups[1].Value;
			processed = processed.Replace(placeholder, ResolvePlaceholder(placeholder));
		}

		return processed;
	}

	private string ResolvePlaceholder(string placeholder)
	{
		placeholder = placeholder
			.Replace("{", "")
			.Replace("}", "");

		if (!placeholder.Contains(':'))
			return GetStringPlaceholder(placeholder, string.Empty);

		var parts = placeholder.Split(":");
		var key = parts[0];
		var format = parts[1].Replace("'", "");

		return GetStringPlaceholder(key, format);
	}

	private string GetStringPlaceholder(string key, string args)
	{
		if (!Placeholders.TryGetValue(key, out var rawValue))
			return string.Empty;

		return rawValue switch
		{
			string strPlaceholder => strPlaceholder,
			int intValue => CastAs.String(intValue, args),
			long longValue => CastAs.String(longValue, args),
			bool boolValue => CastAs.String(boolValue),
			DateTime dateValue => CastAs.String(dateValue, args),
			float floatValue => CastAs.String(floatValue, args),
			double doubleValue => CastAs.String(doubleValue, args),
			_ => $"(UNSUPPORTED:{rawValue.GetType().Name})"
		};
	}
}
