using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using ViewModel;
using RestSharp;

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


        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePassword viewmodel)
        {
            if (ModelState.IsValid)
            {
               viewmodel.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
               var res =  await  accountManager.ChangePassword(viewmodel);
               if (res.Succeeded) {
                    return RedirectToAction("login");
               }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(viewmodel);
                }
            }
            return View(viewmodel);

        }

        [HttpGet]
        public IActionResult GetResetPasswordCode() { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var code = await accountManager.GetResetPasswordCode(email);
            if (string.IsNullOrEmpty(code)) {
                ModelState.AddModelError("", "Sorry Your Account Email Not Registered Yet");
                return View();
            }
            else
            {
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 25)
                {
                    Credentials = new NetworkCredential
                    ("6d2456ad82c2ca", "b1101effba9f2b"),
                    EnableSsl = true
                };
                client.Send("mailtrap@example.com", email,"Check Your Verification Code", $"Your Code is {code}");

                //var client = new RestClient("https://sandbox.api.mailtrap.io/api/send/3138917");
                //var request = new RestRequest();
                //request.AddHeader("Authorization", "Bearer a5f7f5d77da4c8df5d962a21d87a627b");
                //request.AddHeader("Content-Type", "application/json");
                //request.AddParameter(
                //    "application/json",
                //    $@"from: {{email: mailtrap@example.com ,name: Mailtrap Test}}, to:[{{email:h.mohammed.samy@gmail.com}}],subject:Check Your Verification Code,text: {code}",
                //    ParameterType.RequestBody);
                //var response = client.Post(request);

                return View(new UserResetPasswordViewModel() { Email = email});
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = await accountManager.ResetPassword(viewModel);
                if (res.Succeeded)
                {
                    return RedirectToAction("login");
                }
                else {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(viewModel);
        }

    }
}
