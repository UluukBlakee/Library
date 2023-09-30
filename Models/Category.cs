using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните название")]
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
