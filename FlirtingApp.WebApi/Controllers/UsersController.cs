using System;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Photos.Queries.GetUserPhoto;
using FlirtingApp.Application.Users.Commands.CreateUser;
using FlirtingApp.Application.Users.Commands.UpdateUser;
using FlirtingApp.Application.Users.Queries.GetUserDetails;
using FlirtingApp.Application.Users.Queries.GetUsers;
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
		public async Task<IActionResult> UpdateCurrentUser(UpdateUserAdditionalInfoRequest request)
		{
			await _mediator.Send(new UpdateUserAdditionalInfoCommand
			{
				UserId = Guid.Parse(User.FindFirst(AppClaimTypes.UserId).Value),
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
			return Ok(await _mediator.Send(new GetUsersQuery()));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(Guid id)
		{
			return Ok(await _mediator.Send(new GetUserDetailQuery {UserId = id}));
		}

		[HttpGet("{userId}/photos/{photoId}", Name = "GetUserPhoto")]
		public async Task<IActionResult> GetUserPhoto(Guid userId, Guid photoId)
		{
			return Ok(await _mediator.Send(new GetUserPhotoQuery
			{
				UserId = userId,
				PhotoId = photoId
			}));
		}
	}
}