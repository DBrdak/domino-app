using System.Data;

namespace Shared.Domain.Abstractions
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}