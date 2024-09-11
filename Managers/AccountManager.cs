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
            User user = viewModel.ToModel();
           var result =  await userManager.CreateAsync( user , viewModel.Password);
            result =  await userManager.AddToRoleAsync(user, viewModel.Role);
            return result;
        }

        public async Task<SignInResult> Login(UserLoginViewModel viewModel) {

            //is value in (viewModel.LoginMethod) EMAIL OR UserName
            //aly_ahmed
            //alyahmed@iti.gov.eg
            var user = await
                userManager.FindByEmailAsync(viewModel.LoginMethod);

            if (user == null)
            {
                user = await userManager.FindByNameAsync(viewModel.LoginMethod);
                if (user == null)
                {
                    return SignInResult.Failed;
                }
            }
            
            
            return await  signInManager.PasswordSignInAsync(user, viewModel.Password,viewModel.RemeberMe,true); 
            
        }
         public async void SignOut()
         {
            await signInManager.SignOutAsync();
         } 
    }
}
