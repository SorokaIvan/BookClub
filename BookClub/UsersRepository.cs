using BookClub.Models;
using Dapper;
using System.Data;

namespace BookClub
{
    public class UsersRepository
    {
        public IDbConnection _connection;

        public UsersRepository(IDbConnection Connection)
        {
            _connection = Connection;
        }

        public List<UserModel> UserList()
        {
            using (IDbConnection db = _connection)
            {
                var result = db.Query<UserModel>("SELECT * FROM UserBC").ToList();
                return result;
            }
        }

        public void AddUser(UserModel userModel)
        {
            using (IDbConnection db = _connection)
            {
                db.Query<BookModel>($"insert into \"UserBC\" (\"UserName\", \"Password\", \"Role\") " +
                    $"values (N'{userModel.UserName}', N'{userModel.Password}', N'User')");
            }
        }
    }
}
