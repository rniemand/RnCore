using System.Net.Mail;
using System.Text;
using RnCore.DbConfiguration.Attributes;

namespace RnCore.Mailer;

public class RnMailConfig
{
	[StringConfig("host")]
	public string Host { get; set; } = "smtp.gmail.com";

	[IntConfig("port")]
	public int Port { get; set; } = 587;

	[StringConfig("fromName")]
	public string FromName { get; set; } = "";

	[StringConfig("fromAddress")]
	public string FromAddress { get; set; } = "";

	[FilePathConfig("templateDir")]
	public string TemplateDir { get; set; } = "./mail-templates";

	[BoolConfig("enableSSL", true)]
	public bool EnableSsl { get; set; } = true;

	[IntConfig("timeout")]
	public int Timeout { get; set; } = 30000;

	[StringConfig("username")]
	public string Username { get; set; } = string.Empty;

	[StringConfig("password")]
	public string Password { get; set; } = string.Empty;

	public SmtpDeliveryFormat DeliveryFormat { get; set; } = SmtpDeliveryFormat.SevenBit;
	public SmtpDeliveryMethod DeliveryMethod { get; set; } = SmtpDeliveryMethod.Network;
	public Encoding? Encoding { get; set; } = null;

	public bool HasCredentials()
	{
		if (string.IsNullOrWhiteSpace(Username))
			return false;

		return !string.IsNullOrWhiteSpace(Password);
	}
}
