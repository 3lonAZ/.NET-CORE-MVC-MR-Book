using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MR_Book.Models.DBFactory.AppSettingsConfiguration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace MR_Book.Models.Connections
{
    public interface IConnections<T>
    {
        T Connection { get; }
        string ConnectionString { get; }
        bool TestConnection(T typeConnection);
        bool CheckConnectionState { get; }
    }
    public class MyLocalSqlConnection : IConnections<SqlConnection>
    {
        private SqlConnection _connection { get; set; }
        public SqlConnection Connection { get => _connection == null ? new SqlConnection(ConnectionString) : _connection; }


        private readonly IConfiguration _configuration;
        public MyLocalSqlConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ConnectionString
        {
            get
            {
                return  _configuration["ConnectionStrings"];
            }
        }

        public bool CheckConnectionState { get => Connection.State == ConnectionState.Open ? true : false; }
        public bool TestConnection(SqlConnection connection)
        {
            try
            {
                connection = Connection;

                if (CheckConnectionState)
                    connection.Close();

                connection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

    }
}
