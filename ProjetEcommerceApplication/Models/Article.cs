using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetEcommerceApplication.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        [Required]
        public string ArticleName { get; set; }


        public string ArticleDescription { get; set; }

        public string Imageurl { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public  IFormFile ImageFile { get; set; }
    }
}
