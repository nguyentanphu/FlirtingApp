using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlirtingApp.Api.Dtos;
using FlirtingApp.Api.Identity;
using FlirtingApp.Api.Repository;
using FlirtingApp.Api.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.Api.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly UserRepository _userRepository;
	    private readonly IMapper _mapper;
	    public UsersController(UserRepository userRepository, IMapper mapper)
	    {
		    _userRepository = userRepository;
		    _mapper = mapper;
	    }

		[AllowAnonymous]
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

	    [HttpGet]
	    public async Task<IActionResult> GetUsers()
	    {
		    var users = await _userRepository.GetUsers();
		    return Ok(_mapper.Map<IEnumerable<UserForListDto>>(users));
	    }

	    [HttpGet("{id}")]
	    public async Task<IActionResult> GetUser(Guid id)
	    {
		    var user = await _userRepository.GetUser(id);
		    return Ok(_mapper.Map<UserDetail>(user));
	    }

		[HttpPut("{id}")]
	    public async Task<IActionResult> UpdateUser(Guid id, UserForUpdateDto userForUpdateDto)
	    {
		    if (id != Guid.Parse(User.FindFirst("id").Value))
		    {
			    return Unauthorized();
		    }

		    var user = await _userRepository.GetUser(id);
		    _mapper.Map(userForUpdateDto, user);
		    await _userRepository.SaveAll();
		    return NoContent();
	    }

	    [HttpGet("{userId}/photos/{photoId}", Name = "GetUserPhoto")]
	    public async Task<IActionResult> GetUserPhoto(Guid userId, Guid photoId)
	    {
		    var user = await _userRepository.GetUser(userId);
		    var photo = user.GetPhoto(photoId);
		    if (photo == null)
		    {
			    return NotFound("Photo not found!");
		    }

		    return Ok(_mapper.Map<PhotoDto>(photo));
	    }
	}
}