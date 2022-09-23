using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MR_Book.Models.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MR_Book.Areas.Admin.Models.Crud_Operations
{
    public interface ICrud<TModel>
    {
        void Create(TModel model);
        List<TModel> Read();
        void Update(TModel model);
        void Delete();
        void Remove(int id);
    }
    public interface ICrudSpecial<TModel>
    {
        List<TModel> Read();
        void Delete();
        void Remove(int id);

    }
    public class BookFacotry : ICrud<BookModel>
    {
        private readonly IConnections<SqlConnection> _connection;
        private readonly IHostingEnvironment _environment;
        public BookFacotry(IConnections<SqlConnection> connection, IHostingEnvironment environment)
        {
            _environment = environment;
            _connection = connection;
        }
        public void Create(BookModel model)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec CreateBook @book_name,@book_author,@book_language,@book_page_count,@book_category,@book_about,@book_price,@book_release_date,@book_size,@book_image", connection);
                command.Parameters.AddWithValue("@book_name", model.Name);
                command.Parameters.AddWithValue("@book_author", model.Author);
                command.Parameters.AddWithValue("@book_language", model.LanguageId);
                command.Parameters.AddWithValue("@book_page_count", model.PageCount);
                command.Parameters.AddWithValue("@book_category", model.CategoryID);
                command.Parameters.AddWithValue("@book_about", model.About);
                command.Parameters.AddWithValue("@book_price", model.Price);
                command.Parameters.AddWithValue("@book_release_date", model.Release_Date.Value.ToString("MM-dd-yyyy"));
                command.Parameters.AddWithValue("@book_size", model.Size);
                command.Parameters.AddWithValue("@book_image", model.BookIMG.FileName);
                command.ExecuteNonQuery();
                _ = UploadFile(model.BookIMG);
                connection.Close();
            }
        }
        private async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            try
            {
                if (file.Length > 0)
                {
                    string filename = file.FileName;
                    path = Path.GetFullPath(Path.Combine(this._environment.WebRootPath, "book-images"));
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    iscopied = true;
                }
                else
                {
                    iscopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return iscopied;
        }

        public void Remove(int id)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteBookById @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Delete()
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteBook", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<BookModel> Read()
        {
            var bookModel = new List<BookModel>();

            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec GetBook", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookModel.Add(new BookModel
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Author = (string)reader["Author"],
                        PageCount = (int)reader["Page_Count"],
                        About = (string)reader["About"],
                        Price = (double)reader["Price"],
                        Release_Date = Convert.ToDateTime(reader["Release_Date"]),
                        Size = (string)reader["Size"],
                        ImageUrl = (string)reader["Image"],
                        Languages = (string)reader["Language"],
                        Category = (string)reader["Category"],
                        LanguageId = (int)reader["LanguageID"],
                        CategoryID = (int)reader["CategoryID"]
                    });
                }
                connection.Close();
            };

            return bookModel;
        }
        public void Update(BookModel model)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec UpdateBook @name,@author,@language,@page_count,@category,@about,@price,@release_date,@size,@image,@id", connection);
                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@author", model.Author);
                command.Parameters.AddWithValue("@language", model.LanguageId);
                command.Parameters.AddWithValue("@page_count", model.PageCount);
                command.Parameters.AddWithValue("@category", model.CategoryID);
                command.Parameters.AddWithValue("@about", model.About);
                command.Parameters.AddWithValue("@price", model.Price);
                command.Parameters.AddWithValue("@release_date", model.Release_Date.Value.ToString("MM-dd-yyyy"));


                if (model.BookIMG == null)
                    command.Parameters.AddWithValue("@image", DBNull.Value);
                else
                {
                    command.Parameters.AddWithValue("@image", model.BookIMG.FileName);
                    _ = UploadFile(model.BookIMG);
                }



                command.Parameters.AddWithValue("@size", model.Size);
                command.Parameters.AddWithValue("@Id", model.Id);



                command.ExecuteNonQuery();

                connection.Close();
            }
        }

    }
    public class CategoryFactory : ICrud<CategoryModel>
    {
        private readonly IConnections<SqlConnection> _connection;
        public CategoryFactory(IConnections<SqlConnection> connection)
        {
            _connection = connection;
        }
        public void Create(CategoryModel model)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec CreateCategory @category", connection);
                command.Parameters.AddWithValue("@category", model.Category);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete()
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteCategory", connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<CategoryModel> Read()
        {
            List<CategoryModel> category = new();
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec GetCategoryForAdmin", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category.Add(new CategoryModel
                    {
                        Id = (int)reader["Id"],
                        Category = reader["Category"].ToString()
                    });
                }
                connection.Close();

            }
            return category;
        }
        public void Remove(int id)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteCategoryById @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(CategoryModel model)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec UpdateCategory @category,@id", connection);
                command.Parameters.AddWithValue("@category", model.Category);
                command.Parameters.AddWithValue("@id", model.Id);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
    public class LanguageModelFactory : ICrud<LanguageModel>
    {
        private readonly IConnections<SqlConnection> _connection;
        public LanguageModelFactory(IConnections<SqlConnection> connection)
        {
            _connection = connection;
        }
        public void Create(LanguageModel model)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec CreateLanguage @language", connection);
                command.Parameters.AddWithValue("@language", model.Language);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteLanguageById @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<LanguageModel> Read()
        {

            List<LanguageModel> languages = new();

            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec GetLanguageForAdmin", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    languages.Add(new LanguageModel
                    {
                        Id = (int)reader["Id"],
                        Language = reader["Language"].ToString()
                    });
                }
                connection.Close();
            }

            return languages;
        }
        public void Delete()
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteLanguage", connection);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(LanguageModel model)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec UpdateLanguage @language,@id", connection);
                command.Parameters.AddWithValue("@language", model.Language);
                command.Parameters.AddWithValue("@id", model.Id);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
    public class OrdersModelFactory : ICrudSpecial<OrderModel>
    {
        private readonly IConnections<SqlConnection> _connection;
        public OrdersModelFactory(IConnections<SqlConnection> connection)
        {
            _connection = connection;
        }

        public void Delete()
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteOrderDetail", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<OrderModel> Read()
        {
            {
                var orderModel = new List<OrderModel>();

                using (SqlConnection connection = _connection.Connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("exec GetOrderDetail", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        orderModel.Add(new OrderModel
                        {
                            Id = (int)reader["Id"],
                            BookID = (int)reader["Book_ID"],
                            BookName = (string)reader["Name"],
                            FullName = (string)reader["Full_Name"],
                            ContactNumber = (string)reader["Contact_Number"],
                            Count = (byte)reader["Count"],
                            Address = (string)reader["Address"],
                            OrderDate = (DateTime)reader["Order_Date"]
                        });
                    }
                    connection.Close();
                };

                return orderModel;
            }

           
        }
        public void Remove(int id)
        {
            using (SqlConnection connection = _connection.Connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("exec DeleteOrderDetailById @orderId", connection);
                command.Parameters.AddWithValue("orderId", id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
