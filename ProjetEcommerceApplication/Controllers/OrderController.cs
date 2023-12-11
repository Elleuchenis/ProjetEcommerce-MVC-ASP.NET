using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetEcommerceApplication.Models;
using ProjetEcommerceApplication.Repository;


namespace ProjetEcommerceApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly UserManager<IdentityUser> userManager;
        public OrderController(IOrderRepository orderRepository, IArticleRepository articleRepository, UserManager<IdentityUser> userManager)
        {
            this._orderRepository = orderRepository;
            this._articleRepository = articleRepository;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        // GET: OrderCommandController
        public IActionResult Index()
        {
            var listOrder = _orderRepository.GetAll();
           
            return View(listOrder);
        }
        public async Task<IActionResult> MyOrders()
        {
            IdentityUser user = await userManager.GetUserAsync(User);
            
            var listOrder = _orderRepository.FindByOrderByClient(user.Id);
            Console.WriteLine($"la list des ordere pour se user  {listOrder}");
            return View(listOrder);
        }
        // GET: OrderCommandController/Details/5
        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            var article = _articleRepository.GetById(order.ArticleId);
            ViewBag.ArticleId = new SelectList(new List<Article> { article }, "ArticleId", "ArticleName", article.ArticleId);
            return View(order);
        }
        

        // GET: OrderCommandController/Create

        public IActionResult Create(int id)
        {
            var article = _articleRepository.GetById(id);
            var viewModel = new CreateOrderViewModel
            {
                ArticleId = article.ArticleId,
                ArticleName = article.ArticleName,
                ArticleDescription = article.ArticleDescription,
                Imageurl = article.Imageurl,
                Price = article.Price,
                // ... autres propriétés nécessaires pour la commande
            };
            ViewBag.ArticleId = new SelectList(new List<Article> { article }, "ArticleId", "ArticleName", article.ArticleId);
            return View(viewModel);
        }


        // POST: OrderCommandController/Create

        [HttpPost]
        public async Task<IActionResult> CreateAsync(  Order Command)
        {
            var a = _articleRepository.GetById(Command.ArticleId);
            if (Command != null   )
            {
                if (a.Quantity >= Command.QuantityCommand)
                {
                    IdentityUser user = await userManager.GetUserAsync(User);
                    Command.UserId = user.Id;

                    //creation des role et affectation des role
                    _orderRepository.Add(Command);
                    return RedirectToAction("Myorders");
                }
                return RedirectToAction("Myorders");



            }
            return RedirectToAction("Myorders");
        }
        // GET: OrderCommandController/Edit/5
        public IActionResult EditMyOrder(int id)

        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {

                return NotFound();
            }

            ViewBag.ArticleId = new SelectList(_articleRepository.GetAll(), "ArticleId", "ArticleName");
            return View(order);
        }
        [HttpPost]
        public ActionResult EditMyOrder(int id, Models.Order order)
        {
            if (order.Id != id)
            {
                return NotFound();
            }

            if (order.Id == id)
            {
                _orderRepository.EditMyOrder(order);
                return RedirectToAction("MyOrders");
            }

            return View(order);
        }
        [Authorize(Roles = "Admin")]
        // GET: OrderCommandController/Edit/5
        public IActionResult Edit(int id)

        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {

                return NotFound();
            }
            ViewBag.ArticleId = new SelectList(_articleRepository.GetAll(), "ArticleId", "ArticleName");
            return View(order);
        }

        // POST: OrderCommandController/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Models.Order order)
        {
            if (order.Id != id)
            {
                return NotFound();
            }
            
            if (order.Id == id)
            {
                _orderRepository.Edit(order);
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: OrderCommandController/Delete/5
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderCommandController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {   
                return NotFound();
            }
            if ( order.CommandState == "pasencore") {

                _orderRepository.Delete(order);
            }
            
            return RedirectToAction("MyOrders");

        }
    }
}
