using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetEcommerceApplication.Models;
using ProjetEcommerceApplication.Repository;
using Microsoft.AspNetCore.Hosting;

namespace ProjetEcommerceApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IArticleRepository _articleRepository;
        public ArticleController(IArticleRepository articleRepository, IWebHostEnvironment env)
        {
            this._articleRepository = articleRepository;
            _env = env;
        }
        // GET: ArticleController
        public IActionResult Index()
        {
            var articles = _articleRepository.GetAll();
            return View(articles);
        }
        [AllowAnonymous]
        public IActionResult Catalogue()
        {
            var article = _articleRepository.GetAll();
            return View(article);
        }

        // GET: ArticleController/Details/5
        public IActionResult Details(int id)
        {
            var article = _articleRepository.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        [AllowAnonymous]
        public IActionResult DetailsUser(int id)
        {
            var article = _articleRepository.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        // GET: ArticleController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        public IActionResult Create(Article article)
        {
            if (article != null)
            {
               


                // Obtenir le chemin du dossier où vous souhaitez sauvegarder l'image
                var uploadFolder = Path.Combine(_env.WebRootPath, "images");

                // Assurez-vous que le dossier existe, sinon, créez-le
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Générez un nom de fichier unique pour éviter les conflits
                var fileName = Guid.NewGuid().ToString() + "_" + article.ImageFile.FileName;

                // Chemin complet du fichier
                var filePath = Path.Combine(uploadFolder, fileName);

                // Copiez le fichier dans le dossier d'images
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    article.ImageFile.CopyTo(stream);
                }

                // Enregistrez le nom du fichier dans la propriété Imageurl de votre modèle si nécessaire
                
                article.Imageurl = fileName;


                _articleRepository.Add(article);
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        // GET: ArticleController/Edit/5
        public IActionResult Edit(int id)
        {
            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return NotFound();
            }
            Console.WriteLine($"************************************{id}");

            return View(article);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Models.Article article)
        {
            
            
                var uploadFolder = Path.Combine(_env.WebRootPath, "images");

                // Assurez-vous que le dossier existe, sinon, créez-le
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Générez un nom de fichier unique pour éviter les conflits
                var fileName = Guid.NewGuid().ToString() + "_" + article.ImageFile.FileName;

                // Chemin complet du fichier
                var filePath = Path.Combine(uploadFolder, fileName);

                // Copiez le fichier dans le dossier d'images
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    article.ImageFile.CopyTo(stream);
                }

                // Enregistrez le nom du fichier dans la propriété Imageurl de votre modèle si nécessaire

                article.Imageurl = fileName;
                _articleRepository.Edit(article);
                return RedirectToAction("Index");
            

           
        }

        // GET: ArticleController/Delete/5
        public IActionResult Delete(int id)
        {
            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: ArticleController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var article = _articleRepository.GetById(id);

            if (article == null)
            {
                return NotFound();
            }

            _articleRepository.Delete(article);
            return RedirectToAction("Index");

        }
    }
}