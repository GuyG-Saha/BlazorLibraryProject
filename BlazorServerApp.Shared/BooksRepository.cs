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
        public void InsertBook(Book book)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                if (book.Quantity <= 0)
                {
                    book.Available = false;
                }
                string query = @"
                        INSERT INTO Books (Id, Name, AuthorId, Quantity, Price, Available)
                        VALUES (@Id, @Name, @AuthorId, @Quantity, @Price, @Available);
                        ";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Name", book.Name);
                    command.Parameters.AddWithValue("@AuthorId", book.Author.Id);
                    command.Parameters.AddWithValue("@Quantity", book.Quantity);
                    command.Parameters.AddWithValue("@Price", book.Price);
                    command.Parameters.AddWithValue("@Available", book.Available);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}