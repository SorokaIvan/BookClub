using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Введите Логин!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
