using BookClub.Models;

namespace BookClub
{
    public class AccountsRepository
    {
        public bool GetUserByName(List<UserModel> Users, UserModel userModel)
        {
            foreach (var item in Users)
            {
                if (item.UserName == userModel.UserName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
