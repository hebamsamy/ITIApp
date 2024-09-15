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
            //based ion role Add TO Table (Publicher or Auther)
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
        public async Task<IdentityResult> ChangePassword(UserChangePassword viewModel)
        {
            var User = await userManager.FindByIdAsync(viewModel.UserID);

            return await userManager.ChangePasswordAsync(User, viewModel.CurrentPassword, viewModel.NewPassword);
        }
        public async Task<string> GetResetPasswordCode(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return string.Empty;
            }
            else
            {
                return await userManager.GeneratePasswordResetTokenAsync(user);
            }
        }

        public async Task<IdentityResult> ResetPassword(UserResetPasswordViewModel viewModel )
        {
            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync
                     (user, viewModel.Code, viewModel.NewPassword);
            }
            else {
                return IdentityResult.Failed(
                    new IdentityError()
                    {
                        Description = "Sorry In valid Operation !!!"
                    });
            }
        }
    }
}
