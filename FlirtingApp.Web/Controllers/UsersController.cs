using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Requests;
using FlirtingApp.Application.Users.Commands.CreateUser;
using FlirtingApp.Application.Users.Commands.UpdateUser;
using FlirtingApp.Web.Dtos;
using FlirtingApp.Web.Repository;
using FlirtingApp.Web.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.Web.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly IMediator _mediator;
	    public UsersController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

		[AllowAnonymous]
	    [HttpPost]
	    public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
	    {
		    await _mediator.Send(createUserCommand);

		    return NoContent();
	    }

		[HttpPut]
		public async Task<IActionResult> UpdateUser(UpdateUserAdditionalInfoRequest request)
		{
			await _mediator.Send(new UpdateUserAdditionalInfoCommand
			{
				UserId = Guid.Parse(HttpContext.User.FindFirst(AppClaimTypes.UserId).Value),
				Introduction = request.Introduction,
				Interests = request.Interests,
				LookingFor = request.LookingFor,
				City = request.City,
				Country = request.Country
			});
			return NoContent();
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

		//[HttpPut("{id}")]
		//   public async Task<IActionResult> UpdateUser(Guid id, UserForUpdateDto userForUpdateDto)
		//   {
		//    if (id != Guid.Parse(User.FindFirst("id").Value))
		//    {
		//	    return Unauthorized();
		//    }

		//    var user = await _userRepository.GetUser(id);
		//    _mapper.Map(userForUpdateDto, user);
		//    await _userRepository.SaveAll();
		//    return NoContent();
		//   }

		//   [HttpGet("{userId}/photos/{photoId}", Name = "GetUserPhoto")]
		//   public async Task<IActionResult> GetUserPhoto(Guid userId, Guid photoId)
		//   {
		//    var user = await _userRepository.GetUser(userId);
		//    var photo = user.GetPhoto(photoId);
		//    if (photo == null)
		//    {
		//	    return NotFound("Photo not found!");
		//    }

		//    return Ok(_mapper.Map<PhotoDto>(photo));
		//   }
	}
}