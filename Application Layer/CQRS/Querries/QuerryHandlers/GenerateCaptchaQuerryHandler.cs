using Application_Layer.CQRS.Querries.Querry;
using Application_Layer.Profile;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.CQRS.Querries.QuerryHandlers
{
    public class GenerateCaptchaQuerryHandler : IRequestHandler<GenerateCaptchaQuerry, CaptchaResult>
    {
        private readonly ICaptchaServices _captchaServices;
        public GenerateCaptchaQuerryHandler(ICaptchaServices captchaServices)
        {
            _captchaServices = captchaServices;
        }
        

        Task<CaptchaResult> IRequestHandler<GenerateCaptchaQuerry, CaptchaResult>.Handle(GenerateCaptchaQuerry request, CancellationToken cancellationToken)
        {
            var (captchaText, captchaImageData) = _captchaServices.GenerateCaptcha();

            // Return captcha text and image data
            return Task.FromResult(new CaptchaResult
            {
                CaptchaText = captchaText,
                CaptchaImageData = captchaImageData
            });
        }
    }
}
