using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class MovieModel
    {
        [Range(1, 18, ErrorMessage = "Выберете жанр!")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Это Фильм или Сериал?")]
        public string TypeOfVideoContent { get; set; }
        [Required(ErrorMessage = "Введите название Фильма / Сериала!")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать больше 50 символов")]
        public string TitleMovie { get; set; }
        [Required(ErrorMessage = "Введите Режиссера!")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать больше 50 символов")]
        public string Regisseur { get; set; }
        [Required(ErrorMessage = "Введите описание!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Вставьте URL ссылку на картинку!")]
        public string ImgMovie { get; set; }
        [Range(1, 10, ErrorMessage = "Поставьте рейтинг!")]
        public int Rating { get; set; }
        [ValidateNeverAttribute]
        public string Link { get; set; }
        [Required(ErrorMessage = "Вставьте ссылку на трейлер!")]
        public string Trailer { get; set; }
        [ValidateNeverAttribute]
        public string UserNameAddMovie { get; set; }
        [Range(1960, 2023, ErrorMessage = "Вставьте корректный год релиза!")]
        public int ReleaseDate { get; set; }
        [ValidateNeverAttribute]
        public string Genre { get; set; }
        [ValidateNeverAttribute]
        public int MovieId { get; set; }
    }
}
