﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    

    public class LoginModel
    {
        public string CaptchaId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CaptchaSolution { get; set; }
        
    }
}



    

