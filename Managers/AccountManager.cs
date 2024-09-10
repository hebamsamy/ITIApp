using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Managers
{
    public class AccountManager :MainManager<User>
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public AccountManager(ProjectContext context 
            ,UserManager<User> _userManager, SignInManager<User> _signmanager
            ) : base(context) {
            signInManager = _signmanager;
            userManager = _userManager;
        }


        public async Task<IdentityResult> Register(UserRegisterViewModel viewModel )
        {
           return  await userManager.CreateAsync(viewModel.ToModel(), viewModel.Password);
        }

        public async Task<SignInResult> Login(UserLoginViewModel viewModel) {
            return await  signInManager.PasswordSignInAsync(
                 viewModel.UserName, viewModel.Password,viewModel.RemeberMe,true); 
            
        }
         public async void SignOut()
         {
            await signInManager.SignOutAsync();
         } 
    }
}
