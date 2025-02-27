using Microsoft.Data.SqlClient;

namespace Data
{
    public class DataAccess
    {
        public readonly SqlConnection conn;

        public DataAccess(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }
    }
}
