using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Users : IdentityUser
    {
        
        public int UserId { get; set; }

        
        public  string Name { get; set; } = string.Empty;

        
        public string UserPasswordHash { get; set; } = string.Empty;

        
    }
}
