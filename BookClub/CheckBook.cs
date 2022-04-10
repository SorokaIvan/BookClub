using BookClub.Models;

namespace BookClub
{
    public class CheckBook
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
    }
}
