using ProjetEcommerceApplication.Models;

namespace ProjetEcommerceApplication.Repository
{
    public interface IArticleRepository
    {
        IList<Article> GetAll();
        Article GetById(int id);
        void Add(Article a);
        void Edit(Article a);
        void Delete(Article a);
        IList<Article> FindByArticleName(String articleName);
    }
}
