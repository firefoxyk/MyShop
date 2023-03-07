namespace MyShop.Models
{
    public class ShoppingCart//тут пишем все свойства, которые надо хранить в сессии
    {
        public int ProductId { get; set; }//в сессии надо хранить id продукта для добавления в козрину
    }
}
