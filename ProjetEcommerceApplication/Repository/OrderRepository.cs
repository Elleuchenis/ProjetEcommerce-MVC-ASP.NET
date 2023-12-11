using ProjetEcommerceApplication.Models;


namespace ProjetEcommerceApplication.Repository
{
    public class OrderRepository : IOrderRepository
    {
        readonly CommerceContext context;
        public OrderRepository(CommerceContext context)
        {
            this.context = context;
        }
        public void Add(Order ordc)

        {
            var a = context.Articles.Find(ordc.ArticleId);
            a.Quantity = a.Quantity-ordc.QuantityCommand;
            ordc.Id = 0;
            ordc.Totaleprice = ordc.QuantityCommand * a.Price;
            ordc.CommandState = "pasencore";
            Console.WriteLine($"Le ID du utilisateur :{ordc.UserId}");
            context.Order.Add(ordc);
            context.Articles.Update(a);
            context.SaveChanges();
        }

        public void Delete(Order ordc)
        {
            Order o1 = context.Order.Find(ordc.Id);
            if (o1 != null)
            {
                var a=context.Articles.Find(ordc.ArticleId);
                a.Quantity=a.Quantity+ordc.QuantityCommand;
                context.Articles.Update(a);
                context.Order.Remove(ordc);
                context.SaveChanges();
            }
        }
        public void EditMyOrder(Order ordc)
        {
            Order o1 = context.Order.Find(ordc.Id);
            if (o1 != null)
            {
                var a = context.Articles.Find(o1.ArticleId);
                o1.Phone = ordc.Phone;
                o1.QuantityCommand = ordc.QuantityCommand;
                o1.Totaleprice = ordc.QuantityCommand * a.Price;
                o1.UserName = ordc.UserName;
                o1.Address = ordc.Address;
                context.Order.Update(o1);
                context.SaveChanges();
            }

        }
        public void Edit(Order ordc)
        {
            Order o1 = context.Order.Find(ordc.Id);
            if (o1 != null)
            {
                
                
               
               
                o1.CommandState =ordc.CommandState;
                context.Order.Update(o1);
                context.SaveChanges();
            }

        }

        public IList<Order> FindByOrderByClient(string id)
        {
            return context.Order.Where(o => o.UserId==id).ToList();
        }

        public IList<Order> GetAll()
        {
            return context.Order.ToList();
        }

        public Order GetById(int id)
        {
            return context.Order.Find(id);
        }
    }
}
