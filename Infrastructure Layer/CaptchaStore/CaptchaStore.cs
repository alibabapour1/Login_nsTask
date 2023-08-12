using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Layer.CaptchaStore
{
    public class CaptchaStore
    {
        private readonly Dictionary<string, string> _captchaTextByUserId = new Dictionary<string, string>();
        private readonly TimeSpan _expirationTime = TimeSpan.FromMinutes(5);

        public void StoreCaptchaText(string userId, string captchaText)
        {
            _captchaTextByUserId[userId] = captchaText;
            
            Task.Delay(_expirationTime).ContinueWith(_ => _captchaTextByUserId.Remove(userId));
        }

        public string GetCaptchaText(string userId)
        {
            _captchaTextByUserId.TryGetValue(userId, out var captchaText);
            return captchaText;
        }
    }
}
