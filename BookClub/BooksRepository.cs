using BookClub.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BookClub
{
    public class BooksRepository
    {
        private readonly IConfiguration _config;

        public BooksRepository(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public List<BookModel> GetBooks()
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<BookModel>($"SELECT * FROM Books").ToList();
                return resultAll;
            }
        }

        public List<BookModel> GetBooksUser(string name)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<BookModel>($"SELECT * FROM Books WHERE UserNameAddBook = '{name}'").ToList();
                return resultAll;
            }
        }


        public List<BookModel> BookList(string genre)
        {
            var allGenre = "All";
            var connection = CreateConnection();
            using (connection)
            {
                if (genre == allGenre)
                {
                    return GetBooks();
                }
                else
                {
                    var resultGenre = connection.Query<BookModel>($"SELECT * FROM Books inner join StyleBook on Books.GenreId = StyleBook.GenreId WHERE GenreName = N'{genre}'").ToList();
                    return resultGenre;
                }
            }
        }

        public void AddBook(BookModel book)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"insert into \"Books\" (\"GenreId\", \"TitleBook\", \"Author\", \"Description\", \"Rating\", \"Link\", \"img\", \"UserNameAddBook\", \"Genre\") " +
                    $"values (N'{book.GenreId}', N'{book.TitleBook}', N'{book.Author}', N'{book.Description}', N'{book.Rating}', N'{book.Link}', N'{book.Img}', N'{book.UserNameAddBook}', N'{book.Genre}')");
            }
        }

        public void DeleteBookUser(string titleBook)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"delete from Books where TitleBook = N'{titleBook}'").ToList();
            }
        }

        public void EditBookUser(BookModel book)
        {
            var connection = CreateConnection();
            using (connection)
            {
                connection.Query<BookModel>($"UPDATE Books SET GenreId = {book.GenreId}, TitleBook = N'{book.TitleBook}', Author = N'{book.Author}', Description = N'{book.Description}', Rating = {book.Rating}, Link = N'{book.Link}', img = N'{book.Img}', Genre = N'{book.Genre}' WHERE BookId = {book.BookId}").ToList();
            }
        }

        public BookModel GetBookToEdit(int id)
        {
            var connection = CreateConnection();
            using (connection)
            {
                var resultAll = connection.Query<BookModel>($"SELECT * FROM Books WHERE BookId = {id}").FirstOrDefault();
                return resultAll;
            }
        }
    }
}
