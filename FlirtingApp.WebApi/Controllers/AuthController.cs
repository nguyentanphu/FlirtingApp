using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.ExchangeTokens;
using FlirtingApp.Application.Auth.Commands.Login;
using FlirtingApp.Application.Auth.Commands.Logout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.Controllers
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
	    public async Task<IActionResult> Login(LoginRequest loginRequest)
	    {
		    var loginResponse = await _mediator.Send(new LoginCommand
		    {
				UserName = loginRequest.UserName,
				Password = loginRequest.Password,
				RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString()
		    });
		    return Ok(loginResponse);
	    }

	    [HttpPost("logout")]
		[Authorize]
	    public async Task<IActionResult> Logout()
	    {
		    await _mediator.Send(new LogoutCommand
		    {
			    RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString()
		    });
		    return Ok();
	    }

		[HttpPost("exchangeTokens")]
	    public async Task<IActionResult> ExchangeTokens(ExchangeTokensRequest exchangeTokensRequest)
	    {
		    var exchangeTokensResult = await _mediator.Send(new ExchangeTokensCommand
		    {
				RefreshToken = exchangeTokensRequest.RefreshToken,
				AccessToken = exchangeTokensRequest.AccessToken,
				RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString()
			});
		    return Ok(exchangeTokensResult);
	    }
    }
}