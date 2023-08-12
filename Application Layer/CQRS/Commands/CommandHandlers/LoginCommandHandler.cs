using Application_Layer.CQRS.Commands.Command;
using Azure.Core;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.CQRS.Commands.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, IActionResult>
    {
        private readonly ICaptchaServices _captchaServices;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IMemoryCache _memoryCache;

        public LoginCommandHandler(
            ICaptchaServices captchaServices,
            IAuthenticationServices authenticationServices,
            IMemoryCache memoryCache)
        {
            _captchaServices = captchaServices;
            _authenticationServices = authenticationServices;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            
            string captchaText;
            if (!_memoryCache.TryGetValue(model.CaptchaId, out captchaText))
            {
                return new BadRequestObjectResult("Invalid or missing captcha ID.");
            }

            _memoryCache.Remove(model.CaptchaId);

            if (!_captchaServices.ValidateCaptchaSolution(model.CaptchaSolution, captchaText))
            {
                return new BadRequestObjectResult("Invalid captcha solution.");
            }

            bool isCredentialsValid = await _authenticationServices.ValidateUserCredentials(model);

            if (!isCredentialsValid)
            {
                return new UnauthorizedObjectResult("Invalid username or password.");
            }

            

            return new OkObjectResult("Login successful.");
        }
    }
}

