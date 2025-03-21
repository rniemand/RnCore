using System.Text.Json;

namespace RnCore.Sonarr;

public class SonarrApiRequestDumpInfo
{
	public DateTime Date { get; set; } = DateTime.Now;
	public string Url { get; set; } = string.Empty;
	public string Method { get; set; } = string.Empty;
	public object? Request { get; set; } = null;
	public object? Response { get; set; } = null;

	public SonarrApiRequestDumpInfo()
	{
	}

	public SonarrApiRequestDumpInfo(string url, HttpMethod method)
	{
		Url = url;
		Method = method.Method;
	}

	public SonarrApiRequestDumpInfo WithResponse(string responseJson)
	{
		Response = JsonSerializer.Deserialize<object>(responseJson);
		return this;
	}

	public SonarrApiRequestDumpInfo WithResponseObject(object response)
	{
		Response = response;
		return this;
	}

	public SonarrApiRequestDumpInfo WithRequestObject(object request)
	{
		Request = request;
		return this;
	}
}
