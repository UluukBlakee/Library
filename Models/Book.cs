using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Название")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Укажите автора")]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Укажите путь к изображению")]
        [Display(Name = "Фото обложки")]
        public string ImagePath { get; set; }
        [Display(Name = "Год релиза")]
        public int? YearRelease { get; set; }
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        [Display(Name = "Дата добавления")]
        public DateTimeOffset? DateAdded { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
