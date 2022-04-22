using BookClub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksRepository _booksRepository;
        private readonly UsersRepository _usersRepository;
        private readonly GenreReposytory _genreRepository;

        public BooksController(BooksRepository booksRepository, UsersRepository usersRepository, GenreReposytory genreRepository)
        {
            _booksRepository = booksRepository;
            _usersRepository = usersRepository;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult ListBook(string value)
        {
            var model = _booksRepository.BookList($"{value}");
            return View(model);
        }

        public IActionResult AddBook()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBook(BookModel book)
        {
            if (ModelState.IsValid)
            {
                var books = _booksRepository.GetBooks();
                CheckModel checkBook = new CheckModel();
                var value = checkBook.GetBookByName(books, book);

                if(value == false)
                {
                    var user = _usersRepository.GetUser(User.Identity.Name);
                    _usersRepository.IncrementBooksUserCount(user);
                    book.Genre = _genreRepository.GetGenreBook(book.GenreId);
                    _booksRepository.AddBook(book);
                    ViewBag.MessageCompleted = "Книга успешно добавлена!";
                }
                else
                {
                    ViewBag.MessageError = "Такая книга уже есть в каталоге!";
                }
            }
            return View();
        }
    }
}
