using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        [Range(1, int.MaxValue)]
        public string Price { get; set; }
        public string Image { get; set; }



        //Создание связи с табл Catigory
        [Display (Name = "Category Type")]
        public int CategoryId { get; set; }
        [ForeignKey ("CategoryId")]
        public virtual Catigory Catigory { get; set; }

        //Создание связи с табл Application
        [Display(Name = "Application Type")]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Application Application { get; set; }

    }
}
