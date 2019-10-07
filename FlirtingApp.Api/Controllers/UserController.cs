using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlirtingApp.Api.Repository;
using FlirtingApp.Api.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
	    private readonly AppUserRepository _appUserRepository;

	    public UserController(AppUserRepository appUserRepository)
	    {
		    _appUserRepository = appUserRepository;
	    }

	    [HttpPost]
	    public async Task<IActionResult> Create(RegisterUserRequest newUser)
	    {
		    var createdUser = await _appUserRepository.Create(
			    newUser.FirstName,
			    newUser.LastName,
			    newUser.Email,
			    newUser.UserName,
			    newUser.Password
			);

		    return Ok(createdUser);
	    }
    }
}