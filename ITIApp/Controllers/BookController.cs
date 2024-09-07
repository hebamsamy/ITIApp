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
            AddBookViewModel book = bookManager.GetOne(id).ToAddViewModel();

            ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = puplisherManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            return View(book);//ERRRRROR
        }
        [HttpPost]
        public IActionResult Edit(AddBookViewModel data)
        {

            if (ModelState.IsValid)
            {
                data.ImagePaths = new List<string>();
                foreach (IFormFile file in data.Images)
                {
                    string fileName = DateTime.Now.ToFileTime().ToString() + file.FileName;
                    string path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        "Books", fileName
                        );
                    FileStream fileStream = new FileStream(path, FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Close();

                    data.ImagePaths.Add(Path.Combine("Images", "Books", fileName));
                }

                List<string> oldImages = bookManager.GetOne(data.ID!.Value).Attachments.Select(b => b.Path).ToList();

                bookManager.Update(data);

                if (data.KeepOldImages == false)
                {
                    foreach (string item in oldImages)
                    {
                        string oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item);
                        System.IO.File.Delete(oldpath);
                        //ToDo :File Not Deleted
                    }
                }

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
                
                foreach (IFormFile file in data.Images)
                {
                    string fileName = DateTime.Now.ToFileTime().ToString() + file.FileName;
                    string path = Path.Combine(
                        Directory.GetCurrentDirectory() ,
                        "wwwroot",  
                        "Images" , 
                        "Books" ,  fileName
                        );
                    FileStream fileStream = new FileStream(path, FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Close();

                    data.ImagePaths.Add(Path.Combine("Images","Books", fileName));
                }

                
                bookManager.Add(data.ToModel());
              
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

       
        public IActionResult Delete(int id)
        {
            bookManager.Delete(bookManager.GetOne(id));

            return RedirectToAction("index");
        }

    }
}
