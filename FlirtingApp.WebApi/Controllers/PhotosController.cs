using System;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Photos.Commands.CreatePhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlirtingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class PhotosController : ControllerBase
    {
	    private readonly IMediator _mediator;
	    private readonly ICurrentUser _currentUser;

	    public PhotosController(IMediator mediator, ICurrentUser currentUser)
	    {
		    _mediator = mediator;
		    _currentUser = currentUser;
	    }

	    [HttpPost]
	    public async Task<IActionResult> AddPhotoForLoggedInUser([FromForm] CreatePhotoRequest request)
	    {
		    var userId = _currentUser.UserId.Value;

			var photoId = await _mediator.Send(new CreatePhotoCommand
		    {
			    UserId = userId,
			    File = request.File,
			    Description = request.Description
		    });

		    return CreatedAtRoute("GetUserPhoto", new { userId, photoId }, null);
		}

    }
}