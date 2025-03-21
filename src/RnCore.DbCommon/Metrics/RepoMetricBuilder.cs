using RnCore.Metrics;

namespace RnCore.DbCommon;

public class RepoMetricBuilder() : BaseMetricBuilder<RepoMetricBuilder>(Measurement)
{
	public const string Measurement = "repo_call";
	private int _resultsCount;
	private string _repoName = string.Empty;
	private string _repoMethod = string.Empty;
	private string _connection = string.Empty;

	// Constructors
	public RepoMetricBuilder(string repoName, string repoMethod, string connection)
		: this(repoName, repoMethod)
	{
		ForConnection(connection);
	}

	public RepoMetricBuilder(string repoName, string repoMethod)
		: this()
	{
		ForRepo(repoName, repoMethod);
	}

	// Builder methods
	public RepoMetricBuilder ForRepo(string repoName, string repoMethod)
	{
		_repoName = repoName;
		_repoMethod = repoMethod;
		return this;
	}

	public RepoMetricBuilder ForConnection(string connection)
	{
		_connection = connection;
		return this;
	}

	public RepoMetricBuilder WithResultCount(int resultCount)
	{
		_resultsCount = resultCount;
		return this;
	}

	public RepoMetricBuilder CountResult(object? result = null)
	{
		if (result != null)
			_resultsCount += 1;

		return this;
	}

	public override RnMetric Build()
	{
		// Set repo specific Fields and Tags
		AddAction(m => m.SetTag("repo_name", _repoName))
			.AddAction(m => m.SetTag("repo_method", _repoMethod))
			.AddAction(m => m.SetTag("repo_connection", _connection))
			.AddAction(m => m.SetField("results_count", _resultsCount))
			.AddAction(m => m.SetField("call_count", 1));

		// Bake in all other Fields and Tags
		return base.Build();
	}
}
