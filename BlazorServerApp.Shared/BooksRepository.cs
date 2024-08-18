using BlazorServerApp.Shared;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;


namespace BlazorServerDemo.Data
{
    public class BooksRepository
    {
        private readonly string _connectionString;

        public BooksRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Book> GetBooks()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var books = connection.Query<Book>("SELECT * FROM Book").ToList();
                return books;
            }
        }
        public List<Book> GetBooksJoinAuthors()
        {
            var books = new List<Book>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                                SELECT b.Id, b.Name, b.Quantity, b.Price, b.Available, a.Id AS AuthorId, a.Name AS AuthorName
                                FROM Books b
                                INNER JOIN Authors a ON b.AuthorId = a.Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                Id = reader.GetInt64("Id"),
                                Name = reader.GetString("Name"),
                                Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? (int?)null : reader.GetInt32("Quantity"),
                                Price = reader.GetInt32("Price"),
                                Available = reader.GetBoolean("Available"),
                                Author = new Author
                                {
                                    Id = reader.GetInt64("AuthorId"),
                                    Name = reader.GetString("AuthorName")
                                }
                            };
                            books.Add(book);
                        }
                    }
                }
            }
            return books;
        }
    }
}