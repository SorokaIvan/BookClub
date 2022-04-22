using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public class BookModel
    {
        [Range(1, 10, ErrorMessage = "Выберете жанр!")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Введите название книги!")]
        [MaxLength(50, ErrorMessage="Длина не должна превышать больше 50 символов")]
        public string TitleBook { get; set; }

        [Required(ErrorMessage = "Введите автора!")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать больше 50 символов")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Введите описание!")]
        public string Description { get; set; }

        [Range(1, 10, ErrorMessage = "Поставьте рейтинг!")]
        public int Rating { get; set; }

        [ValidateNeverAttribute]
        public string Link { get; set; }

        [Required(ErrorMessage = "Вставьте URL ссылку!")]
        public string Img { get; set; }
        public string UserNameAddBook { get; set; }

        [ValidateNeverAttribute]
        public int BookId { get; set; }
        [ValidateNeverAttribute]
        public string Genre { get; set; }
    }
}
