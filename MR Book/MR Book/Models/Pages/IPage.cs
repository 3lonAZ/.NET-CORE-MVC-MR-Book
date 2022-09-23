using MR_Book.Models.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MR_Book.Models.Pages;
using MR_Book.Models.Pages.ExternalData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MR_Book.Models.Pages.Order;

namespace MR_Book.Models.Pages
{
    public interface IPage
    {
        public List<Book> SelectData();
        public List<ExternalFeature> GetCategories();
        public List<ExternalFeature> GetLanguages();
        public void InsertOrder(OrderDetail orderDetail);
    }
    public class PageFactory : IPage
    {
        private readonly IConnections<SqlConnection> _connections;
        private readonly IExternalFeature _externalFeature;

        public PageFactory(IConnections<SqlConnection> connections,IExternalFeature externalFeature)
        {
            _connections = connections;
            _externalFeature = externalFeature;
        }
        public List<Book> SelectData()
        {
            List<Book> book = new List<Book>();

            using (SqlConnection connection = _connections.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec GetBook", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book.Add(new Book
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Author = (string)reader["Author"],
                        PageCount = (int)reader["Page_Count"],
                        About = (string)reader["About"],
                        Price = (double)reader["Price"],
                        Release_Date = Convert.ToDateTime(reader["Release_Date"]),
                        Size = (string)reader["Size"],
                        Image = (string)reader["Image"],
                        Languages = (string)reader["Language"],
                        Category = (string)reader["Category"]

                    });
                }
                connection.Close();
            }
            return book;
        }
        public void InsertOrder(OrderDetail order)
        {
            using (SqlConnection connection = _connections.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec CreateOrderDetail @book_id,@full_name,@contact_number,@count,@address,@order_date", connection);
                command.Parameters.AddWithValue("@book_id", order.BookID);
                command.Parameters.AddWithValue("@full_name", order.FullName);
                command.Parameters.AddWithValue("@contact_number", order.ContactNumber);
                command.Parameters.AddWithValue("@count", order.Count);
                command.Parameters.AddWithValue("@address", order.Address);
                command.Parameters.AddWithValue("@order_date", DateTime.Now.ToString("yyyy.MM.dd HH:mm"));

                command.ExecuteNonQuery();
            }
        }

        public List<ExternalFeature> GetCategories() => _externalFeature.GetExternalFeature("exec GetCategory", _connections);
        public List<ExternalFeature> GetLanguages() => _externalFeature.GetExternalFeature("exec GetLanguage", _connections);

    }

}
