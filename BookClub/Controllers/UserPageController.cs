using BookClub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.Controllers
{
    public class UserPageController : Controller
    {
        private readonly BooksRepository _booksRepository;
        private readonly UsersRepository _usersRepository;
        private readonly MovieRepository _movieRepository;
        private readonly GenreReposytory _genreRepository;

        public UserPageController(BooksRepository booksRepository, UsersRepository usersRepository, MovieRepository movieRepository, GenreReposytory genreRepository)
        {
            _booksRepository = booksRepository;
            _usersRepository = usersRepository;
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        private UserPageViewModel GetListContentUser()
        {
            UserPageViewModel userPageViewModel = new UserPageViewModel();
            userPageViewModel.books = _booksRepository.GetBooksUser(User.Identity.Name);
            userPageViewModel.Films = _movieRepository.GetFilmsUser(User.Identity.Name);
            userPageViewModel.Serials = _movieRepository.GetSerialsUser(User.Identity.Name);
            userPageViewModel.User = _usersRepository.GetUser(User.Identity.Name);

            return userPageViewModel;
        }

        [HttpGet]

        public IActionResult UserPageView()
        {
            var viewModel = GetListContentUser();
            return View(viewModel);
        }

        public IActionResult DeleteBook(string titleBook)
        {
            var user = _usersRepository.GetUser(User.Identity.Name);
            _usersRepository.DecrementBooksUserCount(user);
            _booksRepository.DeleteBookUser(titleBook);
            return RedirectToAction("UserPageView");
        }

        public IActionResult AddEditBook(int id)
        {

            return View(_booksRepository.GetBookToEdit(id));
        }

        public IActionResult DeleteMovie(string titleMovie, string typeMovie)
        {
            var user = _usersRepository.GetUser(User.Identity.Name);
            _usersRepository.DecrementMoviesUserCount(user, typeMovie);
            _movieRepository.DeleteMovieUser(titleMovie);
            return RedirectToAction("UserPageView");
        }

        [HttpGet]
        public IActionResult AddEditMovie(int id)
        {

            return View(_movieRepository.GetMovieToEdit(id));
        }

        [HttpPost]
        public IActionResult AddEditBook(BookModel book)
        {
            if (ModelState.IsValid)
            {
                book.Genre = _genreRepository.GetGenreBook(book.GenreId);
                _booksRepository.EditBookUser(book);
                ViewBag.MessageCompletedEditBook = "Книга успешно отредактирована!";
            }
            return View(book);
        }

        public IActionResult AddEditMovie(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                movie.Genre = _genreRepository.GetGenreMovie(movie.GenreId);
                _movieRepository.EditMovieUser(movie);
                ViewBag.MessageCompletedEditMovie = "Фильм / Сериал успешно отредактирован!";
            }
            return View(movie);
        }
    }
}
