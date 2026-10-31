using System.Data;

namespace Billora.Subscriptions.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
