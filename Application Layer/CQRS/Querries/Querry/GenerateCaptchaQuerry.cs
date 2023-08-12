using Application_Layer.Profile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.CQRS.Querries.Querry
{
    public class GenerateCaptchaQuerry : IRequest<CaptchaResult>
    {
    }
}
