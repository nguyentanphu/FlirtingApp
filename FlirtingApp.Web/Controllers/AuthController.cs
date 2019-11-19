using System.Threading.Tasks;
using FlirtingApp.Application.Users.Commands.ExchangeTokens;
using FlirtingApp.Application.Users.Commands.Login;
using FlirtingApp.Web.ConfigOptions;
using FlirtingApp.Web.Repository;
using FlirtingApp.Web.RequestModels;
using FlirtingApp.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FlirtingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
	    private readonly IMediator _mediator;

	    public AuthController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

	    [HttpPost("login")]
	    public async Task<IActionResult> Login(LoginCommand loginCommand)
	    {
		    var loginResponse = await _mediator.Send(loginCommand);
		    return Ok(loginResponse);
	    }

	    [HttpPost("exchangeTokens")]
	    public async Task<IActionResult> ExchangeTokens(ExchangeTokensCommand exchangeTokensCommand)
	    {
		    var exchangeTokensResult = await _mediator.Send(exchangeTokensCommand);
		    return Ok(exchangeTokensResult);
	    }
    }
}