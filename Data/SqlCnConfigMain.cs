using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace mpolaris.Data
{
    public class SqlCnConfigMain
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public SqlCnConfigMain(IConfiguration configuration)
        {
            _configuration = configuration;
            //_connectionString = _configuration.GetConnectionString("mysqlconection");
            _connectionString = _configuration.GetConnectionString("sqlconection");
        }

        public IDbConnection CreateConnection()
            //=> new MySqlConnection(_connectionString);
            => new SqlConnection(_connectionString);
    }

    public class SqlCnConfigMainB
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public SqlCnConfigMainB(IConfiguration configuration)
        {
            _configuration = configuration;
            //_connectionString = _configuration.GetConnectionString("mysqlconection");
            _connectionString = _configuration.GetConnectionString("sqlconection_B");
        }

        public IDbConnection CreateConnection()
            //=> new MySqlConnection(_connectionString);
            => new SqlConnection(_connectionString);
    }
}
