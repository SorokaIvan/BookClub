using BookClub.Models;
using Dapper;
using System.Data;

namespace BookClub
{
    public class BooksRepository
    {
        public IDbConnection _connection;

        public BooksRepository(IDbConnection Connection)
        {
            _connection = Connection;
        }

        public List<BookModel> GetBooks()
        {
            using (IDbConnection db = _connection)
            {
                var resultAll = db.Query<BookModel>($"SELECT * FROM Books").ToList();
                return resultAll;
            }
        }

        public List<BookModel> GetBooksUser(string name)
        {
            using (IDbConnection db = _connection)
            {
                var resultAll = db.Query<BookModel>($"SELECT * FROM Books WHERE UserNameAddBook = '{name}'").ToList();
                return resultAll;
            }
        }


        public List<BookModel> BookList(string genre)
        {
            using (IDbConnection db = _connection)
            {
                if (genre == "All")
                {
                    return GetBooks();
                }
                else
                {
                    var resultGenre = db.Query<BookModel>($"SELECT * FROM Books inner join StyleBook on Books.StyleId = StyleBook.StyleId WHERE StyleName = '{genre}'").ToList();
                    return resultGenre;
                }
            }
        }

        public void AddBook(BookModel book)
        {
            using (IDbConnection db = _connection)
            {
                db.Query<BookModel>($"insert into \"Books\" (\"StyleId\", \"TitleBook\", \"Author\", \"Description\", \"Rating\", \"Link\", \"img\", \"UserNameAddBook\") " +
                    $"values (N'{book.StyleId}', N'{book.TitleBook}', N'{book.Author}', N'{book.Description}', N'{book.Rating}', N'{book.Link}', N'{book.Img}', N'{book.UserNameAddBook}')");
            }
        }

        public void DeleteBookUser(string TitleBook)
        {
            using (IDbConnection db = _connection)
            {
                db.Query<BookModel>($"delete from Books where TitleBook = N'{TitleBook}'").ToList();
            }
        }
    }
}
