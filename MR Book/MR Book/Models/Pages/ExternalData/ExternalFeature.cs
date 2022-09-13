using MR_Book.Models.Connections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MR_Book.Models.Pages.ExternalData
{
    public interface IExternalFeature
    {
        string Name { get; set; }
        int Count { get; set; }
        List<ExternalFeature> GetExternalFeature(string procCommand, IConnections<SqlConnection> _connections);
    }
    public class ExternalFeature:IExternalFeature
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public List<ExternalFeature> GetExternalFeature(string procCommand, IConnections<SqlConnection> _connections)
        {
            List<ExternalFeature> externalFeature = new List<ExternalFeature>();

            using (SqlConnection connection = _connections.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procCommand, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    externalFeature.Add(new ExternalFeature
                    {
                        Name = reader["Name"].ToString(),
                        Count = (int)reader["Count"]
                    });
                }
                connection.Close();
            }
            return externalFeature;
        }
    }

}
