using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarModelMgmt.Infrastructure
{
    public class DapperHelper
    {
        private readonly string _connectionString;

        public DapperHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteAsync(string storedProcedureName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedureName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedureName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<object> ExecuteScalarAsync(string storedProcedureName, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
