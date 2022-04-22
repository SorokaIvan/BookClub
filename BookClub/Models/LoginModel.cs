using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите Логин!")]
        [Display(Name = "UserName")]
        public string UserLogin{ get; set; }
        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
