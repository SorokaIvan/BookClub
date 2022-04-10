using BookClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BookClub.Controllers
{
    public class UserPageController : Controller
    {
        private readonly IConfiguration _config;

        public UserPageController(IConfiguration config)
        {
            _config = config;
        }

        private List<BookModel> GetListBooks()
        {
            return BookGetConnection().GetBooksUser(User.Identity.Name);
        }

        private BooksRepository BookGetConnection()
        {
            BooksRepository dbContext = new BooksRepository(new SqlConnection(_config.GetConnectionString("DefaultConnection")));
            return dbContext;
        }

        [HttpGet]


        public IActionResult UserPageView()
        {
            var model = GetListBooks();
            return View(model);
        }

        public IActionResult DeleteBook(string tetileBook)
        {
            BookGetConnection().DeleteBookUser(tetileBook);
            return RedirectToAction("UserPageView");
        }

        public IActionResult EditBook()
        {
            return View();
        }
    }
}
