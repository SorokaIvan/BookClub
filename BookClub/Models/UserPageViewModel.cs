namespace BookClub.Models
{
    public class UserPageViewModel
    {
        public List<BookModel> books { get; set; }
        public UserModel User { get; set; }
        public List<MovieModel> Films { get; set; }
        public List<MovieModel> Serials { get; set; }
    }
}
