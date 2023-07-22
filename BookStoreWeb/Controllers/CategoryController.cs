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
            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("Custom Error", "The Display Order cannot be same as the Name");
            }
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

        //EDIT
        //GET

        //we will retrive an integer which will be id and based on it we have to retrieve the category details
        //and display them
        public IActionResult Edit(int id)
        {
            if(id==null || id == 0)
            {
                return NotFound(); //bc its an invalid id
            }
            //if thats not invalid then we will retrieve the Categories from database
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDb == null) 
            {
                return NotFound();
            }
            return View(categoryFromDb);
            //Next Step from here
            //now we will create a view which will have the category loaded which will look exactly like the create view
            //but the only thing that will be different is it for the Edit page
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Helps and prevents cross site forgery attack
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Custom Error", "The Display Order cannot be same as the Name");
            }
            if (ModelState.IsValid) //if valid create and redirect to index, if not we return back to the view with the object
            {
                //Now that we have the category object that is populated with name and display order,
                //we want to create that record inside our DB. to do that we will use our DbContext

                _db.Categories.Update(obj);  //In order to retrieve Categories we need to use the Add method 
                                          //the obj is what the user populated
                                          //chnaging Add to Update to edit a record.

                _db.SaveChanges(); //to save changes and push to db

                //return View();  rather than returning the view, we want to redirect to the Index action
                return RedirectToAction("Index");
            }
            return View(obj); //if not we return back to the view with the object
        }
    }
}
