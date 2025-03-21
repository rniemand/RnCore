using System.Data;

namespace RnCore.DbCommon;

public interface IDbConnectionFactory
{
	Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
	Task<IDbConnection> CreateNamedConnectionAsync(string connectionName, CancellationToken cancellationToken = default);
}
