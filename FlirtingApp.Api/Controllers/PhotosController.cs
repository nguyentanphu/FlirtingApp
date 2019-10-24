using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FlirtingApp.Api.ConfigOptions;
using FlirtingApp.Api.Dtos;
using FlirtingApp.Api.Models;
using FlirtingApp.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FlirtingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class PhotosController : ControllerBase
    {
	    private readonly CloudinaryCredential _cloudinaryCredential;
	    private readonly UserRepository _userRepository;
	    private readonly IMapper _mapper;
	    private readonly Cloudinary _cloudinary;

	    public PhotosController(
		    IOptions<CloudinaryCredential> cloudinaryCredential,
		    UserRepository userRepository,
		    IMapper mapper
		    )
	    {
		    _cloudinaryCredential = cloudinaryCredential.Value;
		    _userRepository = userRepository;
		    _mapper = mapper;

		    var account = new Account(
			    _cloudinaryCredential.CloudName,
				_cloudinaryCredential.ApiKey,
				_cloudinaryCredential.ApiSecret
		    );

			_cloudinary = new Cloudinary(account);
	    }

	    [HttpPost]
	    public async Task<IActionResult> AddPhotoForLoggedInUser([FromForm]PhotoForCreationDto photoForCreation)
	    {
		    if (photoForCreation.File == null || photoForCreation.File.Length == 0)
		    {
			    return BadRequest("No file to upload");
		    }
		    var userId = Guid.Parse(User.FindFirst("id").Value);
		    var currentUser = await _userRepository.GetUser(userId);

		    var currentFile = photoForCreation.File;
		    ImageUploadResult uploadResult;
		    using (var stream = currentFile.OpenReadStream())
		    {
			    var uploadParams = new ImageUploadParams
			    {
				    File = new FileDescription(currentFile.Name, stream),
					Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
			    };

			    uploadResult = _cloudinary.Upload(uploadParams);
		    }

		    photoForCreation.PublicId = uploadResult.PublicId;
		    photoForCreation.Url = uploadResult.Uri.ToString();

		    var photo = _mapper.Map<Photo>(photoForCreation);

		    currentUser.AddPhoto(photo);
		    await _userRepository.SaveAll();

		    return CreatedAtRoute("GetUserPhoto", new { userId, photoId = photo.Id }, null);
	    }
    }
}