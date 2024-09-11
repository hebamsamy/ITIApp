using Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModel;

namespace ITIApp
{
    public class AccountController : Controller
    {
        private AccountManager accountManager;
        private RoleManager roleManager;
        public AccountController(AccountManager _accountManager,RoleManager _roleManager ) { 
            this.accountManager = _accountManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["list"]  = roleManager.GetAll()
                .Where(r => r.Name != "Admin")
                .Select(r => new SelectListItem(r.Name, r.Name)).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
        {
            ViewData["list"] = roleManager.GetAll()
                .Where(r => r.Name != "Admin")
                .Select(r => new SelectListItem(r.Name, r.Name)).ToList();
           
            if (ModelState.IsValid) { 
                var result =  await accountManager.Register(viewModel);
                if (result.Succeeded) { 
                    return RedirectToAction("Login","Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);   
                    }
                    return View(viewModel);
                }
            }
            else
            {
                return View(viewModel);
            }
        }
        [HttpGet]
        public IActionResult LogIn(string ReturnUrl = "/")
        {
            UserLoginViewModel model = new UserLoginViewModel { ReturnUrl = ReturnUrl };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginViewModel viewModel) {
            if (ModelState.IsValid) {
                var result = await accountManager.Login(viewModel);
                if (result.Succeeded)
                {
                    if(viewModel.ReturnUrl != "/")
                    {
                        return Redirect(viewModel.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "Book");
                    }

                }
                else if(result.IsLockedOut){
                    ModelState.AddModelError("", "Sorry Your Account Is under Review , Try Later!!!");
                    return View(viewModel);
                }
                else
                {
                    ModelState.AddModelError("", "Sorry Your Cridentionals is in valid Try Again!!!");
                    return View(viewModel);
                }
            }
            else { 
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult LogOut() {
            accountManager.SignOut();
            return RedirectToAction("index", "home");
        }





        //private 
    }
}
