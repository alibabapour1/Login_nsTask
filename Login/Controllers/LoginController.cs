using Application_Layer.Services;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly ICaptchaServices _captchaServices;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _captchaExpirationTime = TimeSpan.FromMinutes(5);

        public LoginController(IAuthenticationServices authenticationServices, ICaptchaServices captchaServices, IMemoryCache memoryCache)
        {
            _captchaServices = captchaServices;
            _authenticationServices = authenticationServices;
            _memoryCache = memoryCache;
        }

        [HttpGet("generate-captcha")]
        public IActionResult GenerateCaptcha()
        {
            string captchaId = Guid.NewGuid().ToString();
            var (captchaText, captchaImageData) =  _captchaServices.GenerateCaptcha();

            
             _memoryCache.Set(captchaId, captchaText, _captchaExpirationTime);

            string imageDataUri = $"data:image/png;base64,{Convert.ToBase64String(captchaImageData)}";
            Response.Cookies.Append("CaptchaId", captchaId, new CookieOptions
            {
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict
            });



            return File(captchaImageData, "image/png");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            string captchaText;
            model.CaptchaId = Request.Cookies["CaptchaId"];

            if (string.IsNullOrEmpty(model.CaptchaId) || !_memoryCache.TryGetValue(model.CaptchaId, out captchaText))
            {
                return BadRequest("Invalid or missing captcha ID.");
            }

            _memoryCache.Remove(model.CaptchaId);

            if (!_captchaServices.ValidateCaptchaSolution(model.CaptchaSolution, captchaText))
            {
                return BadRequest("Invalid captcha solution.");
            }

            bool isCredentialsValid = await _authenticationServices.ValidateUserCredentials(model);

            if (!isCredentialsValid)
            {
                return Unauthorized("Invalid username or password.");
            }

            // TODO: Generate JWT token or perform any other logic for successful login

            return Ok("Login successful.");
        }

        [HttpPost("Register")]
        public async Task<bool> RegisterUser([FromBody]LoginModel model)
        {

            return await _authenticationServices.RegisterUser(model);

            

            
            
            
        }






    }
}
