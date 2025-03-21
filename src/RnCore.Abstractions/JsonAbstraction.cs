using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace RnCore.Abstractions;

public interface IJsonAbstraction
{
	TValue? Deserialize<TValue>([StringSyntax(StringSyntaxAttribute.Json)] string json, JsonSerializerOptions? options = null);
	string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null);
}

public class JsonAbstraction : IJsonAbstraction
{
	public TValue? Deserialize<TValue>(string json, JsonSerializerOptions? options = null) =>
		JsonSerializer.Deserialize<TValue>(json);

	public string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null) =>
		JsonSerializer.Serialize(value, options);
}
