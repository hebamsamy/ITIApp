using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.Security.Claims;
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

        [Authorize(Roles = "Admin,Publisher,Auther")]
        public IActionResult Index(
            string searchText, decimal price, int subjectId = 0, int publisherId = 0,
            string columnName = "Id", bool IsAscending = false,
            int PageSize = 6, int PageNumber = 1)
        {

            if( User.HasClaim(i=>i.Type ==ClaimTypes.Role && i.Value == "Publisher"))
            {
                string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(UserID))
                {
                    publisherId = puplisherManager.GetAll().Where(p => p.UserId == UserID).FirstOrDefault()!.ID;
                }
            }

            //call database
            Pagination<List<BookViewModel>>list = bookManager.Get(searchText,price,subjectId,publisherId,columnName,IsAscending, PageSize, PageNumber);
            //pass data to ui
            ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString(),s.ID==subjectId)).ToList();
            ViewData["Publishers"] = puplisherManager.GetAll()
                .Select(s => new SelectListItem($"{s.User.FirstName} {s.User.LastName}", s.ID.ToString(),s.ID == publisherId)).ToList();
           
            ViewData["price"] = price;
            ViewData["searchText"] = searchText;
            ViewData["publisherId"] = publisherId;
            ViewData["subjectId"] = subjectId;

            return View("booklist",list);
        }
        [Authorize]
        public IActionResult GetDetails(int id)
        {
            BookViewModel book = bookManager.GetOne(id).ToViewModel();
            return View(book);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            AddBookViewModel book = bookManager.GetOne(id).ToAddViewModel();

            ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = puplisherManager.GetAll()
                .Select(s => new SelectListItem($"{s.User.FirstName} {s.User.LastName}", s.ID.ToString())).ToList();
            return View(book);//ERRRRROR
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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

                List<string> oldImages =
                    bookManager.GetOne(data.ID!.Value)
                    .Attachments.Select(b => b.Path).ToList();

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
                    .Select(s => new SelectListItem($"{s.User.FirstName} {s.User.LastName}", s.ID.ToString())).ToList();
                return View(data);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ViewData["Subjects"] = subjectManager.GetAll()
                .Select(s => new SelectListItem(s.Name, s.ID.ToString())).ToList();
            ViewData["Publishers"] = puplisherManager.GetAll()
                .Select(s => new SelectListItem($"{s.User.FirstName} {s.User.LastName}", s.ID.ToString())).ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

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
                    .Select(s => new SelectListItem($"{s.User.FirstName} {s.User.LastName}", s.ID.ToString())).ToList();
                return View(data);
            }
        }

        [Authorize(Roles ="Admin")]
        [AuditBookDeletion (Order = 1)]
        public IActionResult Delete(int id)
        {
            bookManager.Delete(bookManager.GetOne(id));

            return RedirectToAction("index");
        }

    }
}
