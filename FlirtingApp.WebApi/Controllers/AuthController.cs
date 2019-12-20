using System.Threading.Tasks;
using FlirtingApp.Application.Auth.Commands.ExchangeTokens;
using FlirtingApp.Application.Auth.Commands.Login;
using FlirtingApp.Application.Auth.Commands.Logout;
using FlirtingApp.WebApi.ApiPresenters;
using FlirtingApp.WebApi.RequestModels;
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
	    private readonly LoginPresenter _loginPresenter;
	    public AuthController(IMediator mediator, LoginPresenter loginPresenter)
	    {
		    _mediator = mediator;
		    _loginPresenter = loginPresenter;
	    }

	    [HttpPost("login")]
	    public async Task<IActionResult> Login(LoginRequest loginRequest)
	    {
			await _mediator.Send(new LoginCommand
			{
				UserName = loginRequest.UserName,
				Password = loginRequest.Password,
				RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
				OutputPort = _loginPresenter
			});

			return _loginPresenter.Result;
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