using Microsoft.Data.SqlClient;

namespace KJWT.SharedKernel.Persistence;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}