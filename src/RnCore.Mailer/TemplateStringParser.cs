using System.Text.RegularExpressions;

namespace RnCore.Mailer;

public interface ITemplateStringParser
{
	string Parse(string template);
}

public class TemplateStringParser : ITemplateStringParser
{
	private static Regex RX_DATE = new("(\\{date:([^\\}]+)\\})", RegexOptions.Compiled);

	public string Parse(string template)
	{
		template = ProcessDatePlaceholders(template);

		return template;
	}

	private static string ProcessDatePlaceholders(string template)
	{
		if (!RX_DATE.IsMatch(template))
			return template;

		var now = DateTime.Now;

		do
		{
			var match = RX_DATE.Match(template);
			template = template.Replace(match.Groups[1].Value, now.ToString(match.Groups[2].Value));
		} while (RX_DATE.IsMatch(template));

		return template;
	}
}
