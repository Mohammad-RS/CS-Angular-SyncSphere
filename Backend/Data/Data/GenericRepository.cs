using Dapper;
using Data.Repositories;
using System.Reflection;

namespace Data.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DataAccess _dataAccess;

        public GenericRepository(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            string table = GetTableName<T>();
            string query = $"SELECT * FROM [dbo].[{table}]";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<T>(query);
            }
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            string table = GetTableName<T>();
            string query = $"SELECT * FROM [dbo].[{table}] WHERE Id = @Id";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.QuerySingleAsync<T>(query, new { Id = id });
            }
        }

        public async Task<bool> DeleteByIdAsync<T>(int id)
        {
            string table = GetTableName<T>();
            string query = $"DELETE FROM [dbo].[{table}] WHERE Id = @Id";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.ExecuteAsync(query, new { Id = id }) > 0;
            }
        }

        public async Task<int> CreateAsync<T>(T createModel)
        {
            Type type = typeof(T);
            string table = type.Name.Replace("Table", "");
            PropertyInfo[] properties = type.GetProperties();

            List<string> fields = new();
            List<string> parameters = new();
            string output = string.Empty;

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    output = "OUTPUT INSERTED.Id";
                    continue;
                }

                fields.Add($"[{property.Name}]");
                parameters.Add($"@{property.Name}");
            }

            string csvFields = string.Join(", ", fields);
            string csvParams = string.Join(", ", parameters);

            string query = $"INSERT INTO [dbo].[{table}] ({csvFields}) {output} VALUES ({csvParams})";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.ExecuteScalarAsync<int>(query, createModel);
            }
        }

        public async Task<bool> UpdateByIdAsync<T>(T updateModel)
        {
            Type type = typeof(T);
            string table = type.Name.Replace("Table", "");
            PropertyInfo[] properties = type.GetProperties();

            List<string> equals = new();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    continue;
                }

                equals.Add($"[{property.Name}] = @{property.Name}");
            }

            string csvEquals = string.Join(", ", equals);

            string query = $"UPDATE [dbo].[{table}] SET {csvEquals} WHERE Id = @Id";

            using (var conn = _dataAccess.conn)
            {
                await conn.OpenAsync();
                return await conn.ExecuteAsync(query, updateModel) > 0;
            }
        }

        private string GetTableName<T>()
        {
            Type type = typeof(T);
            // return type.Name.Replace("Table", "");
            return type.Name;
        }
    }
}
