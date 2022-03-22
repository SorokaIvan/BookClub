using BookClub.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using System.Data;

namespace BookClub
{
    public class DbContext
    {
        public IDbConnection _connection;

        public DbContext(IDbConnection Connection)
        {
            _connection = Connection;
        }

        public List<BookModel> BookList(string style)
        {
            using (IDbConnection db = _connection)
            {
                var result = db.Query<BookModel>($"SELECT * FROM Books inner join StyleBook on Books.StyleId = StyleBook.StyleId WHERE StyleName = '{style}'").ToList();
                return result;
            }
        }

        public void AddBook(BookModel book)
        {
            using (IDbConnection db = _connection)
            {
                db.Query<BookModel>($"insert into \"Books\" (\"StyleId\", \"TitleBook\", \"Author\", \"Description\", \"Rating\", \"Link\", \"img\", \"UserNameAddBook\") " +
                    $"values ('{book.StyleId}', '{book.TitleBook}', '{book.Author}', '{book.Description}', '{book.Rating}', '{book.Link}', '{book.Img}', '{book.UserNameAddBook}')");
            }
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
                    $"values ('{userModel.UserName}', '{userModel.Password}', 'User')");
            }
        }
    }
}
