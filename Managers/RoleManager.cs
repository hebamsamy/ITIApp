using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class RoleManager :MainManager<IdentityRole>
    {
        Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        public RoleManager(ProjectContext context,
            Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager
            ) :base(context) {
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> Add(string RoleName)
        {
            var role = new IdentityRole { Name = RoleName };
             return await roleManager.CreateAsync(role);
        }

    }
}
