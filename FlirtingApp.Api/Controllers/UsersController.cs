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
    public class UsersController : ControllerBase
    {
	    private readonly UserRepository _userRepository;

	    public UsersController(UserRepository userRepository)
	    {
		    _userRepository = userRepository;
	    }

	    [HttpPost("create")]
	    public async Task<IActionResult> Create(RegisterUserRequest newUser)
	    {
		    if (await _userRepository.IsExist(newUser.UserName))
		    {
			    return BadRequest("User name is already exist!");
		    }
		    var createdUser = await _userRepository.Create(
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