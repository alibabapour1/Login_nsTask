using Application_Layer.CQRS.Commands.Command;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.CQRS.Commands.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IActionResult>
    {
        private readonly IAuthenticationServices _authenticationServices;

        public RegisterCommandHandler(IAuthenticationServices authenticationServices)
        {
           _authenticationServices = authenticationServices;
        }
        public async Task<IActionResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            if (model == null) { return new BadRequestObjectResult("Inavlid Inputs"); }

            await _authenticationServices.RegisterUser(model);
            return new OkObjectResult("Login successful");
        }
    }
}
