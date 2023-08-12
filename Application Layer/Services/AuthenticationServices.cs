using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application_Layer.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationServices(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> RegisterUser(LoginModel model)
        {

            var Identityuser = new IdentityUser { UserName = model.Username };
            var res = await _userManager.CreateAsync(Identityuser, model.Password);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            return res.Succeeded;

            
            

        }

        public async Task<bool> ValidateUserCredentials(LoginModel model)
        {

            var IdentityUser = await _userManager.FindByNameAsync(model.Username);

            if (IdentityUser == null) return false;

            

            return await _userManager.CheckPasswordAsync(IdentityUser, model.Password);
        }
    }

}
