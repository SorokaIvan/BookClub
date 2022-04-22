using BookClub.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BookClub
{
    public class GenreReposytory
    {
        private readonly IConfiguration _config;

        public GenreReposytory(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public string GetGenreMovie(int genreId)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<GenreModel>($"SELECT * FROM GenreFilmSerial where GenreId = '{genreId}'").FirstOrDefault();
                return resultAll.GenreName;
            }
        }

        public string GetGenreBook(int genreId)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<GenreModel>($"SELECT * FROM StyleBook where GenreId = '{genreId}'").FirstOrDefault();
                return resultAll.GenreName;
            }
        }
    }
}
