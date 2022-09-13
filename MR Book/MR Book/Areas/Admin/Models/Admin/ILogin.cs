using MR_Book.Models.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MR_Book.Areas.Admin.Models.Admin
{

    public interface ILogin
    {
        public bool CheckLogin(AdminLog adminLog);
    }
    public class Login : ILogin
    {
        private readonly IConnections<SqlConnection> _connection;
        public Login(IConnections<SqlConnection> connection)
        {
            _connection = connection;
        }

        public bool CheckLogin(AdminLog adminLog)
        {

            SqlConnection connection = _connection.Connection;
            connection.Open();
            SqlCommand command = new SqlCommand($"select *from admin_log where Username = '{adminLog.Username}' and Password = '{adminLog.Password}'", connection);
            bool result = command.ExecuteScalar() != null ? true : false;

            connection.Close();
            return result;
        }
    }
}
