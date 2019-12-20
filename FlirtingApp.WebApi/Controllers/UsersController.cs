using System;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Photos.Queries.GetUserPhoto;
using FlirtingApp.Application.Users.Commands.CreateUser;
using FlirtingApp.Application.Users.Commands.UpdateUser;
using FlirtingApp.Application.Users.Queries.GetUserDetails;
using FlirtingApp.Application.Users.Queries.GetUsers;
using FlirtingApp.WebApi.ApiPresenters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly IMediator _mediator;
	    private readonly ICurrentUser _currentUser;
	    private readonly CreateUserPresenter _createUserPresenter;

	    public UsersController(IMediator mediator, ICurrentUser currentUser, CreateUserPresenter createUserPresenter)
	    {
		    _mediator = mediator;
		    _currentUser = currentUser;
		    _createUserPresenter = createUserPresenter;
	    }

		[AllowAnonymous]
	    [HttpPost]
	    public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
	    {
		    createUserCommand.OutputPort = _createUserPresenter;
			await _mediator.Send(createUserCommand);

			return _createUserPresenter.Result;
	    }

		[HttpPatch]
		public async Task<IActionResult> UpdateCurrentUser(UpdateUserAdditionalInfoRequest request)
		{
			await _mediator.Send(new UpdateUserAdditionalInfoCommand
			{
				UserId = _currentUser.UserId.Value,
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

		[HttpGet("{id}", Name = "GetUser")]
		public async Task<IActionResult> GetUser(Guid id)
		{
			return Ok(await _mediator.Send(new GetUserDetailQuery {Id = id}));
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