using BookClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BookClub.Controllers
{
    public class BooksController : Controller
    {
        private readonly IConfiguration _config;

        public BooksController(IConfiguration config)
        {
            _config = config;
        }

        private List<BookModel> GetListBooks()
        {
            return BookGetConnection().GetBooks();
        }

        private BooksRepository BookGetConnection()
        {
            BooksRepository dbContext = new BooksRepository(new SqlConnection(_config.GetConnectionString("DefaultConnection")));
            return dbContext;
        }

        [HttpGet]
        public IActionResult ListBook(string value)
        {
            var model = BookGetConnection().BookList($"{value}");
            return View(model);
        }

        public IActionResult BookCompleted()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult ErrorAddBook()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBook(BookModel book)
        {
            if (ModelState.IsValid)
            {
                CheckBook checkBook = new CheckBook();
                var value = checkBook.GetBookByName(GetListBooks(), book);

                if(value == false)
                {
                    BookGetConnection().AddBook(book);
                    return RedirectToAction("BookCompleted");
                }
                else
                {
                    return RedirectToAction("ErrorAddBook");
                }
            }
            return View();
        }
    }
}
