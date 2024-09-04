using Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using ViewModel;

namespace ITIApp.Controllers
{
    public class BookController : Controller
    {
        
        BookManager bookManager;
        PuplisherManager puplisherManager ;
        SubjectManager subjectManager;
        public BookController(BookManager bookManager, PuplisherManager puplisherManager, SubjectManager subjectManager)
        {
            this.bookManager = bookManager;
            this.puplisherManager = puplisherManager;
            this.subjectManager = subjectManager;
        }

        public IActionResult Index(
            string searchText, decimal price, 
            string columnName = "Id", bool IsAscending = false,
            int PageSize = 5, int PageNumber = 1)
        {
            //call database
            List<BookViewModel> list = bookManager.Get(searchText,price,columnName,IsAscending, PageSize, PageNumber);
            //pass data to ui
            
            return View("booklist",list);
        }

        public IActionResult GetDetails(int id)
        {
            BookViewModel book = bookManager.GetOne(id).ToViewModel();
            return View(book);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book book = bookManager.GetOne(id);

            ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = puplisherManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            return View(book);//ERRRRROR
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = puplisherManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel data) {

            if (ModelState.IsValid)
            {
                //data valid
                //add
                bookManager.Add(new Book
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
              
                //return to list(index)
                return RedirectToAction("index");
            }
            else
            {
                //data not valid
                //return to the same page with validation
                ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
                ViewData["Publishers"] = puplisherManager.GetAll()
                    .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
                return View(data);
            }
        }


    }
}
