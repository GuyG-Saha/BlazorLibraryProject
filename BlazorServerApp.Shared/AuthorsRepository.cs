using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace BlazorServerApp.Shared
{
    public class AuthorsRepository
    {
        private readonly string _connectionString;
        public AuthorsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Author> GetAuthors()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Author>("SELECT * FROM Authors").ToList();
            }
        }
    }
}
