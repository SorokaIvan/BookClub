using BookClub.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BookClub
{
    public class UsersRepository
    {
        private readonly IConfiguration _config;

        public UsersRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public List<UserModel> UserList()
        {
            var connection = CreateConnection();
            using (connection)
            {
                var result = connection.Query<UserModel>("SELECT * FROM UserBC").ToList();
                return result;
            }
        }

        public void AddUser(UserModel userModel)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"insert into \"UserBC\" (\"UserName\", \"UserLogin\", \"UserGender\", \"Password\", \"Role\", \"DateOfBirth\", \"BooksCount\", \"FilmsCount\", \"SerialsCount\", \"GamesCount\") " +
                    $"values (N'{userModel.UserName}', N'{userModel.UserLogin}', N'{userModel.UserGender}', N'{userModel.Password}', N'User', N'{userModel.DateOfBirth.ToString("MM/dd/yyyy")}', '{userModel.BooksCount}', '{userModel.FilmsCount}', '{userModel.SerialsCount}', '{userModel.GamesCount}')");
            }
        }


        public UserModel GetUser(string login)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var result = connection.Query<UserModel>($"SELECT * FROM UserBC WHERE UserLogin = '{login}'").FirstOrDefault();
                return result;
            }
        }

        public void IncrementBooksUserCount(UserModel user)
        {
            user.BooksCount++;
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE UserBC SET BooksCount = {user.BooksCount} WHERE UserLogin = '{user.UserLogin}'").ToList();
            }
        }

        public void IncrementMoviesUserCount(UserModel user, string typeMovie)
        {
            if(typeMovie == "Фильм")
            {
                IncrementFilmUserCount(user);
            }
            if(typeMovie == "Сериал")
            {
                IncrementSerialUserCount(user);
            }
        }

        public void IncrementFilmUserCount(UserModel user)
        {
            user.FilmsCount++;
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE UserBC SET FilmsCount = {user.FilmsCount} WHERE UserLogin = '{user.UserLogin}'").ToList();
            }
        }
        public void IncrementSerialUserCount(UserModel user)
        {
            user.SerialsCount++;
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE UserBC SET SerialsCount = {user.SerialsCount} WHERE UserLogin = '{user.UserLogin}'").ToList();
            }
        }



        public void DecrementBooksUserCount(UserModel user)
        {
            user.BooksCount--;
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE UserBC SET BooksCount = {user.BooksCount} WHERE UserLogin = '{user.UserLogin}'").ToList();
            }
        }


        public void DecrementMoviesUserCount(UserModel user, string typeMovie)
        {
            if (typeMovie == "Фильм")
            {
                DecrementFilmsUserCount(user);
            }
            if (typeMovie == "Сериал")
            {
                DecrementSerialsUserCount(user);
            }
        }

        public void DecrementFilmsUserCount(UserModel user)
        {
            user.FilmsCount--;
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE UserBC SET FilmsCount = {user.FilmsCount} WHERE UserLogin = '{user.UserLogin}'").ToList();
            }
        }

        public void DecrementSerialsUserCount(UserModel user)
        {
            user.SerialsCount--;
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE UserBC SET SerialsCount = {user.SerialsCount} WHERE UserLogin = '{user.UserLogin}'").ToList();
            }
        }
    }
}
