using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlirtingApp.Api.ConfigOptions;
using FlirtingApp.Api.Identity;
using FlirtingApp.Api.RequestModels;
using FlirtingApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FlirtingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
	    private readonly AuthService _authService;
	    private readonly AuthOptions _authOptions;

	    public AuthController(AuthService authService, IOptions<AuthOptions> authOptions)
	    {
		    _authService = authService;
		    _authOptions = authOptions.Value;
	    }

	    [HttpPost("login")]
	    public async Task<IActionResult> Login(LoginRequest loginRequest)
	    {
		    var result = await _authService.Login(loginRequest.UserName, loginRequest.Password, HttpContext.Connection.RemoteIpAddress.ToString());
		    return Ok(result);
	    }

	    [HttpPost("refreshtoken")]
	    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
	    {
		    return Ok(await _authService.ExchangeRefreshToken(
			    refreshTokenRequest.AccessToken,
				refreshTokenRequest.RefreshToken,
				_authOptions.JwtSecret,
				HttpContext.Connection.RemoteIpAddress.ToString()
		    ));
	    }
    }
}