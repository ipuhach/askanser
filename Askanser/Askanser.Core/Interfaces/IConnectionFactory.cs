using System.Data.SqlClient;

namespace Askanser.Core.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
