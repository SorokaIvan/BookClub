using BookClub.Models;

namespace BookClub
{
    public class CheckModel
    {
        public bool GetBookByName(List<BookModel> books, BookModel book)
        {
           var bookUpper = book.TitleBook.ToUpper();
            foreach (var item in books)
            {
                var listBookUpper = item.TitleBook.ToUpper();

                if (listBookUpper == bookUpper)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetMovieByName(List<MovieModel> movies, MovieModel movie)
        {
            var movieUpper = movie.TitleMovie.ToUpper();
            foreach (var item in movies)
            {
                var listMovieUpper = item.TitleMovie.ToUpper();

                if (listMovieUpper == movieUpper)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
