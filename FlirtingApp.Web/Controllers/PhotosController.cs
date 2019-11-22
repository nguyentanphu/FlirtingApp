using System;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Photos.Commands.CreatePhoto;
using FlirtingApp.Web.ConfigOptions;
using FlirtingApp.Web.Dtos;
using FlirtingApp.Web.Models;
using FlirtingApp.Web.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FlirtingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class PhotosController : ControllerBase
    {
	    private readonly IMediator _mediator;

	    public PhotosController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

	    [HttpPost]
	    public async Task<IActionResult> AddPhotoForLoggedInUser([FromForm] CreatePhotoRequest request)
	    {
		    var userId = Guid.Parse(User.FindFirst(AppClaimTypes.UserId).Value);
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