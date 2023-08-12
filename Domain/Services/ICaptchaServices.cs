using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICaptchaServices
    {
        bool ValidateCaptchaSolution(string Solution,string CaptchaText);
        (string captchaText, byte[] captchaImage) GenerateCaptcha();


    }
}
