using BookClub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace BookClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private DbContext GetConnection()
        {
            DbContext dbContext = new DbContext(new SqlConnection(_config.GetConnectionString("DefaultConnection")));
            return dbContext;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult AboutUsView()
        {
            return View();
        }

        public IActionResult ListBook(string value)
        {
            var model = GetConnection().BookList($"{value}");
            return View(model);
        }

        public IActionResult BookCompleted()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddBook(BookModel book)
        {
            if (ModelState.IsValid)
            {
               GetConnection().AddBook(book);
               return RedirectToAction("BookCompleted");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult AuthorizedPage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult UserPage()
        {
            return View();
        }
    }
}