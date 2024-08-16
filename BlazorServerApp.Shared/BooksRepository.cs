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
    }
}