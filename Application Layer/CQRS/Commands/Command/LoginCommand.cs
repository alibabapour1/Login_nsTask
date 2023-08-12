using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.CQRS.Commands.Command
{
    public class LoginCommand : IRequest<IActionResult>
    {
        public LoginModel Model { get; set; }
    }
}
