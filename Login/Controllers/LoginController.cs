using Application_Layer.CQRS.Commands.Command;
using Application_Layer.CQRS.Querries.Querry;
using Application_Layer.Services;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _captchaExpirationTime = TimeSpan.FromMinutes(5);




        public LoginController( IMediator mediator, IMemoryCache memoryCache)
        {

            _memoryCache = memoryCache;
            _mediator = mediator;

            
        }

        [HttpGet("generate-captcha")]
        public async Task<IActionResult> GenerateCaptcha()
        {
            string captchaId = Guid.NewGuid().ToString();

          

            var result = await _mediator.Send(new GenerateCaptchaQuerry());
            string imageDataUri = $"data:image/png;base64,{Convert.ToBase64String(result.CaptchaImageData)}";
            _memoryCache.Set(captchaId, result.CaptchaText,_captchaExpirationTime );
            Response.Cookies.Append("CaptchaId", captchaId, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });



            return  File(result.CaptchaImageData, "image/png");


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            model.CaptchaId = Request.Cookies["CaptchaId"];


            var query = new LoginCommand { Model = model };
            return await _mediator.Send(query);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody]LoginModel model)
        {
             var query = new RegisterCommand { Model = model };
             return await _mediator.Send(query);





        }






    }
}
