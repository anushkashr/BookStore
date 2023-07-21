using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;

            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Helps and prevents cross site forgery attack
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid) //if valid create and redirect to index, if not we return back to the view with the object
            {
                //Now that we have the category object that is populated with name and display order,
                //we want to create that record inside our DB. to do that we will use our DbContext

                _db.Categories.Add(obj);  //In order to retrieve Categories we need to use the Add method 
                                          //the obj is what the user populated

                _db.SaveChanges(); //to save changes and push to db

                //return View();  rather than returning the view, we want to redirect to the Index action
                return RedirectToAction("Index"); 
            }
            return View(obj); //if not we return back to the view with the object
        }
    }
}
