using Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace ITIApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private RoleManager roleManager;
        public RoleController(RoleManager _roleManager) { 
            roleManager = _roleManager;
        }
        [HttpGet]
        public IActionResult add()
        {
            ViewData["list"] = roleManager.GetAll()
                .Select(e=> new RoleViewModel { ID = e.Id, Name = e.Name}).ToList();
            ViewBag.Success = 0;
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> add(RoleViewModel roleView)
        {
            var result =  await roleManager.Add(roleView.Name);
            if (result.Succeeded)
            {
                ViewBag.Success = 1;
            }
            else
            {
                ViewBag.Success = 2;
            }
            ViewData["list"] = roleManager.GetAll()
               .Select(e => new RoleViewModel { ID = e.Id, Name = e.Name }).ToList();
            return View();
        }
    }
}
