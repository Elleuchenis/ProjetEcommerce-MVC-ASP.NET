using ProjetEcommerceApplication.Models;

namespace ProjetEcommerceApplication.Repository
{
    public interface IOrderRepository
    {
        IList<Order> GetAll();
        Order GetById(int id);
        void Add(Order ordc);
        void Edit(Order ordc);
        void Delete(Order ordc);
        IList<Order> FindByOrderByClient(string id);
        public void EditMyOrder(Order ordc);
    }
}
