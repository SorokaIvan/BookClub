using BookClub.Models;

namespace BookClub
{
    public class AccountsRepository
    {
        public bool GetUserByLogin(List<UserModel> users, UserModel userModel)
        {
            foreach (var item in users)
            {
                if (item.UserLogin == userModel.UserLogin)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
