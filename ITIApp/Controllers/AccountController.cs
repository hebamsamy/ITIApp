using Managers;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace ITIApp
{
    public class AccountController : Controller
    {
        private AccountManager accountManager;
        public AccountController(AccountManager _accountManager) { 
            this.accountManager = _accountManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
        {
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
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginViewModel viewModel) {
            if (ModelState.IsValid) {
                var result = await accountManager.Login(viewModel);
                if (result.Succeeded)
                {
                    ///Ues Return URL
                    return RedirectToAction("index", "book");
                }
                else {
                    //if (result.IsNotAllowed|| result.IsLockedOut)
                    ModelState.AddModelError("", "Sorry Your Account Is under Review , Try Later!!!");
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
    }
}
