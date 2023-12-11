using ProjetEcommerceApplication.Models;

namespace ProjetEcommerceApplication.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        readonly CommerceContext context;
        public ArticleRepository(CommerceContext context)
        {
            this.context = context;
        }
        public void Add(Article a)
        {
            context.Articles.Add(a);
            context.SaveChanges();

        }

        public void Delete(Article a)
        {
            Article a1 = context.Articles.Find(a.ArticleId);
            if (a1 != null)
            {
                context.Articles.Remove(a1);
                context.SaveChanges();
            }
        }

        public void Edit(Article a)
        {
            Article a1 = context.Articles.Find(a.ArticleId);
            if (a1 != null)
            {
                a1.ArticleName = a.ArticleName;
                a1.ArticleDescription = a.ArticleDescription;
                a1.Price = a.Price;
                a1.Quantity = a.Quantity;
                a1.Imageurl = a.Imageurl;
                context.SaveChanges();
            }
        }

        public IList<Article> FindByArticleName(string articleName)
        {
            return context.Articles.Where(a => a.ArticleName.Contains(articleName)).ToList();
        }

        public IList<Article> GetAll()
        {
            return context.Articles.OrderBy(s => s.ArticleName).ToList();
        }

        public Article GetById(int id)
        {
            return context.Articles.Find(id);
        }
    }
}
