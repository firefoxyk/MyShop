using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Application
    {
        [Key]//Обозначение что ApplicationId - ключ
        public int ApplicationId { get; set; } //Столбец ApplicationId талицы Application
        [DisplayName("Application Name")]
        [Required]//показать что поле ApplicationName обязательное для заполнения 
        public string ApplicationName { get; set; }//Столбец ApplicationName талицы Application
    }
}
