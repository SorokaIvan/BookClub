using BookClub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly BooksRepository _booksRepository;
        private readonly UsersRepository _usersRepository;
        private readonly MovieRepository _movieRepository;

        public AdminPageController(BooksRepository booksRepository, UsersRepository usersRepository, MovieRepository movieRepository)
        {
            _booksRepository = booksRepository;
            _usersRepository = usersRepository;
            _movieRepository = movieRepository;
        }

        private List<UserModel> GetListUsers()
        {
            return _usersRepository.UserList();
        }

        private List<BookModel> GetListBooks()
        {
            return _booksRepository.GetBooks();
        }

        private List<MovieModel> GetListMovies()
        {
            return _movieRepository.GetMovies();
        }

        public IActionResult AdminPage()
        {
            AdminPageViewModel combinedBookUserModel = new AdminPageViewModel();
            combinedBookUserModel.Books = GetListBooks();
            combinedBookUserModel.Users = GetListUsers();
            combinedBookUserModel.Movies = GetListMovies();
            return View(combinedBookUserModel);
        }
    }
}
