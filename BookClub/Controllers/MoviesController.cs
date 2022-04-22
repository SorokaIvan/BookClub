using BookClub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieRepository _movieRepository;
        private readonly UsersRepository _usersRepository;
        private readonly GenreReposytory _genreRepository;

        public MoviesController(MovieRepository movieRepository, UsersRepository usersRepository, GenreReposytory genreRepository)
        {
            _movieRepository = movieRepository;
            _usersRepository = usersRepository;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult ListMovies(string value)
        {
            var model = _movieRepository.ListMovies(value);
            return View(model);
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                CheckModel checkBook = new CheckModel();
                var value = checkBook.GetMovieByName(_movieRepository.GetMovies(), movieModel);

                if (value == false)
                {
                    var user = _usersRepository.GetUser(User.Identity.Name);
                    _usersRepository.IncrementMoviesUserCount(user, movieModel.TypeOfVideoContent);
                    movieModel.Genre = _genreRepository.GetGenreMovie(movieModel.GenreId);
                    _movieRepository.AddMovie(movieModel);
                    ViewBag.MessageCompleted = "Фильм / Сериал успешно добавлен!";
                }
                else
                {
                    ViewBag.MessageError = "Такой Фильм / Сериал уже есть в каталоге!";
                }
            }
            return View();
        }
    }
}
