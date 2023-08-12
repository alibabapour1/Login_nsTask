using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Profile
{
    public class CaptchaResult
    {
        public string CaptchaText { get; set; }
        public byte[] CaptchaImageData { get; set; }
    }
}
