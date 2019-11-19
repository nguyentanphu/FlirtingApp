using System.Threading.Tasks;
using FlirtingApp.Application.Common.Requests;
using FlirtingApp.Application.Users.Commands.ExchangeTokens;
using FlirtingApp.Application.Users.Commands.Login;
using FlirtingApp.Web.ConfigOptions;
using FlirtingApp.Web.Repository;
using FlirtingApp.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LoginRequest = FlirtingApp.Web.RequestModels.LoginRequest;

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