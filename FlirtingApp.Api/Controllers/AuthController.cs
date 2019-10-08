using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.RequestModels;
using FlirtingApp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
	    private readonly AuthService _authService;

	    public AuthController(AuthService authService)
	    {
		    _authService = authService;
	    }

	    [HttpPost]
	    public async Task<IActionResult> Login(LoginRequest loginRequest)
	    {
		    var result = await _authService.Login(loginRequest.UserName, loginRequest.Password, HttpContext.Connection.RemoteIpAddress.ToString());
		    return Ok(result);
	    }
    }
}