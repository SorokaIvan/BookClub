using BookClub.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BookClub
{
    public class MovieRepository
    {

        private readonly IConfiguration _config;

        public MovieRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public List<MovieModel> GetMovies()
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies").ToList();
                return resultAll;
            }
        }

        public List<MovieModel> GetFilms()
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies WHERE TypeOfVideoContent = N'Фильм'").ToList();
                return resultAll;
            }
        }

        public List<MovieModel> GetSerials()
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies WHERE TypeOfVideoContent = N'Сериал'").ToList();
                return resultAll;
            }
        }

        public List<MovieModel> GetFilmsUser(string name)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies WHERE UserNameAddMovie = '{name}' AND TypeOfVideoContent = N'Фильм'").ToList();
                return resultAll;
            }
        }

        public List<MovieModel> GetSerialsUser(string name)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies WHERE UserNameAddMovie = '{name}' AND TypeOfVideoContent = N'Сериал'").ToList();
                return resultAll;
            }
        }

        public List<MovieModel> ListMovies(string genre)
        {
            var allGenre = "All";
            var connection = CreateConnection();
            using (connection)
            {
                if (genre == allGenre)
                {
                    return GetMovies();
                }
                else
                {
                    var resultGenre = connection.Query<MovieModel>($"SELECT * FROM Movies inner join GenreFilmSerial on Movies.GenreId = GenreFilmSerial.GenreId WHERE GenreName = N'{genre}'").ToList();
                    return resultGenre;
                }
            }
        }

        public MovieModel GetMovieModel(string titleMovie)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies WHERE TitleMovie = N'{titleMovie}'").FirstOrDefault();
                return resultAll;
            }
        }


        public void AddMovie(MovieModel movie)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<MovieModel>($"insert into \"Movies\" (\"TypeOfVideoContent\", \"TitleMovie\", \"Regisseur\", \"Description\", \"ImgMovie\", \"Rating\", \"Link\", \"Trailer\", \"UserNameAddMovie\", \"ReleaseDate\", \"Genre\", \"GenreId\") " +
                    $"values (N'{movie.TypeOfVideoContent}', N'{movie.TitleMovie}', N'{movie.Regisseur}', N'{movie.Description}', N'{movie.ImgMovie}', '{movie.Rating}', N'{movie.Link}', N'{movie.Trailer}', N'{movie.UserNameAddMovie}', '{movie.ReleaseDate}', N'{movie.Genre}', N'{movie.GenreId}')");
            }
        }

        public void DeleteMovieUser(string titleMovie)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<MovieModel>($"delete from Movies where TitleMovie = N'{titleMovie}'").ToList();
            }
        }

        public MovieModel GetMovieToEdit(int id)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<MovieModel>($"SELECT * FROM Movies WHERE MovieId = {id}").FirstOrDefault();
                return resultAll;
            }
        }


        public void EditMovieUser(MovieModel movie)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<MovieModel>($"UPDATE Movies SET GenreId = {movie.GenreId}, TypeOfVideoContent = N'{movie.TypeOfVideoContent}', TitleMovie = N'{movie.TitleMovie}', Regisseur = N'{movie.Regisseur}', Description = N'{movie.Description}', ImgMovie = N'{movie.ImgMovie}', Rating = {movie.Rating}, Link = N'{movie.Link}', Trailer = N'{movie.Trailer}', ReleaseDate = {movie.ReleaseDate}, Genre = N'{movie.Genre}' WHERE MovieId = {movie.MovieId}").ToList();
            }
        }
    }
}
