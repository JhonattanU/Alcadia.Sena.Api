using System.Data;

namespace Alcadia.Sena.Repository
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
