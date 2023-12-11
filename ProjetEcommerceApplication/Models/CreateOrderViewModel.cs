namespace ProjetEcommerceApplication.Models
{
    public class CreateOrderViewModel
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }
        public string Imageurl { get; set; }
        public float Price { get; set; }
        public int QuantityCommand { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
