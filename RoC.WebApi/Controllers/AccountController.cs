using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RoC.Application.Logic.Account;
using RoC.Infrastructure.Auth;
using RoC.WebApi.Application.Auth;
using RoC.WebApi.Application.Response;

namespace RoC.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {

        public AccountController(ILogger<AccountController> logger, 
            IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrentAccount()
        {
            var data = await _mediator.Send(new CurrentAccountQuery.Request() { });
            return Ok(data);
        }




    }
}
