using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Введите свое Имя!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Введите Логин!")]
        public string UserLogin { get; set; }
        [Required(ErrorMessage = "Выберите пол!")]
        public string UserGender { get; set; }
        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [ValidateNeverAttribute]
        public string Role { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите дату рождения!")]
        public DateTime DateOfBirth { get; set; }
        [ValidateNeverAttribute]
        public int BooksCount { get; set; }
        [ValidateNeverAttribute]
        public int FilmsCount { get; set; }
        [ValidateNeverAttribute]
        public int SerialsCount { get; set; }
        [ValidateNeverAttribute]
        public int GamesCount { get; set; }

    }
}
