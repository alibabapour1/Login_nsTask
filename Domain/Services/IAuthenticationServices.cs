using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Services
{
    public interface IAuthenticationServices 
    {
        Task<bool> ValidateUserCredentials(LoginModel model);
        Task<bool> RegisterUser(LoginModel model);
        
    }
}
