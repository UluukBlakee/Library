using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните фамилию")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Заполните почту")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Заполните контактный телефон")]
        [Display(Name = "Контактный телефон")]
        public string? ContactPhone { get; set; }
    }
}
