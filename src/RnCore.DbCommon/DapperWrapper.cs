using Dapper;
using System.Data;

namespace RnCore.DbCommon;

public interface IDapperWrapper
{
	Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, CommandDefinition command);
}

public class DapperWrapper : IDapperWrapper
{
	public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, CommandDefinition command) => cnn.QueryAsync<T>(command);
}
