using Microsoft.AspNetCore.Mvc;

namespace ITIApp
{
    public class AccountController : Controller
    {
        //public IActionResult Welcome()
        //{
        //    var contect = new ContentResult();
        //    contect.Content = "Welcome to My Site";
        //    return  contect ;
        //}

        //public IActionResult Welcome2()
        //{
        //    return new JsonResult(new{  id = 1, name ="ITI"});
        //}

        public IActionResult login()
        {
            //in folder Views / is there File Login.cshtml 
            return View();
        }
    }
}
