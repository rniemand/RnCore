using System.Text;
using System.Web;

namespace RnCore.Sonarr;

public class SonarrUrlBuilder(string baseUrl)
{
	private readonly Dictionary<string, string> _query = [];
	private string _path = string.Empty;

	public SonarrUrlBuilder ForEndpoint(string path)
	{
		_path = path;
		return this;
	}

	public SonarrUrlBuilder WithSegment(string segment)
	{
		if (string.IsNullOrWhiteSpace(_path))
			throw new RnSonarrException("Cannot add a path segment to an empty path");

		if (string.IsNullOrWhiteSpace(segment))
			throw new RnSonarrException("Provided path segment was null or empty!");

		_path += "/" + segment;
		return this;
	}

	public SonarrUrlBuilder WithSegment(int segment) => WithSegment(segment.ToString("D"));

	public SonarrUrlBuilder WithQueryParameter(string key, string value)
	{
		_query[key] = value;
		return this;
	}

	public SonarrUrlBuilder WithQueryParameter(string key, bool value) =>
		WithQueryParameter(key, value ? "true" : "false");

	public string Build()
	{
		var sb = new StringBuilder(baseUrl.EndsWith('/') ? baseUrl : baseUrl + '/');
		if (!string.IsNullOrWhiteSpace(_path)) sb.Append(_path);

		var queryParams = _query
			.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}")
			.ToArray();
		if (queryParams.Length > 0) sb.Append($"?{string.Join("&", queryParams)}");

		return sb.ToString();
	}
}
