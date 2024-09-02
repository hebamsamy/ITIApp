using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace ITIApp.Controllers
{
    public class BookController : Controller
    {
        ProjectContext context = new ProjectContext();
        public IActionResult Index()
        {
            //call database
            List<Book> list = context.Books.ToList();
            //pass data to ui
            ViewBag.Subjects = context.Subjects.ToList();
            ViewData["Subjects"] = context.Subjects.ToList();
            return View("booklist",list);
        }

        public IActionResult GetDetails(int id)
        {
            Book book = context.Books.Find(id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book book = context.Books.Find(id);

            ViewData["Subjects"] = context.Subjects
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = context.Publishers
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            return View(book);//ERRRRROR
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Subjects"] = context.Subjects
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = context.Publishers
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel data) {

            if (ModelState.IsValid)
            {
                //data valid
                //add
                context.Books.Add(new Book
                {
                    Isbn = data.Isbn,
                    Title = data.Title,
                    SubjectId = data.SubjectId,
                    Summary = data.Summary,
                    Price = data.Price,
                    PublicationDate = data.PublicationDate,
                    PublisherId = data.PublisherId,
                    PageCount = data.PageCount,
                    Notes = data.Notes,
                });
                context.SaveChanges();
                //return to list(index)
                return RedirectToAction("index");
            }
            else
            {
                //data not valid
                //return to the same page with validation
                ViewData["Subjects"] = context.Subjects
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
                ViewData["Publishers"] = context.Publishers
                    .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
                return View(data);
            }
        }


    }
}
