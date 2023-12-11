using System.ComponentModel.DataAnnotations;

namespace ProjetEcommerceApplication.Models
{
    public class Order
    {
  
        public int Id { get; set; }

        public int QuantityCommand { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
  
        public float Totaleprice { get; set; }
        public string CommandState { get; set; }
        public string UserId { get; set; }
        
        public string Phone {  get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
    }
}
