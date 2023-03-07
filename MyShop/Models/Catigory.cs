using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Catigory
    {
        [Key]//Обозначение что CategoryId - ключ
        public int CategoryId { get; set; } //Столбец CategoryId талицы Catigory
        [DisplayName("Category Name")]
        [Required]//показать что поле CategoryName обязательное для заполнения 
        public string CategoryName { get; set; }//Столбец CategoryName талицы Catigory
        [DisplayName("Display Order")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Порядк отображения катигории должен быть больше 0")]//От 1 до nt.MaxValue иначе ошибка с текстом ErrorMessage
        public int DisplayOrder { get; set; }// Столбец DisplayOrder талицы Catigory
    }
}
