namespace RnCore.Mailer;

public static class CastAs
{
	public static bool Boolean(string s, bool fallback = false)
	{
		if (string.IsNullOrWhiteSpace(s))
			return fallback;

		return s.ToLower().Trim() switch
		{
			"0" or "false" => false,
			"1" or "true" => true,
			_ => fallback
		};
	}

	public static string String(int value, string? format) => value.ToString(string.IsNullOrWhiteSpace(format) ? "D" : format);
	public static string String(long value, string? format) =>  value.ToString(string.IsNullOrWhiteSpace(format) ? "D" : format);
	public static string String(bool value) => value ? "true" : "false";
	public static string String(DateTime value, string? format) => value.ToString(string.IsNullOrWhiteSpace(format) ? "s" : format);
	public static string String(float value, string? format) => value.ToString(string.IsNullOrWhiteSpace(format) ? "G" : format);
	public static string String(double value, string? format) => value.ToString(string.IsNullOrWhiteSpace(format) ? "G" : format);
}
