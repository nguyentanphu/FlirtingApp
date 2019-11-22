using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.System;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Photos.Commands.CreatePhoto
{
	public class CreatePhotoCommand: CreatePhotoRequest, IRequest<Guid>
	{
		public Guid UserId { get; set; }
	}

	public class CreatePhotoCommandHandler: IRequestHandler<CreatePhotoCommand, Guid>
	{
		private readonly ICloudinary _cloudinary;
		private readonly IAppDbContext _context;
		public CreatePhotoCommandHandler(ICloudinary cloudinary, IAppDbContext context)
		{
			_cloudinary = cloudinary;
			_context = context;
		}

		public async Task<Guid> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
		{
			var user = await _context.Users
				.Include(u => u.Photos)
				.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
			if (user == null)
			{
				throw new ResourceNotFoundException("User", request.UserId);
			}

			using var fileStream = request.File.OpenReadStream();
			var cloudinaryUploadOptions = new CloudinaryUploadOptions
			{
				FileName = request.File.FileName,
				FileStream = fileStream
			};
			var uploadResult = await _cloudinary.Upload(cloudinaryUploadOptions, cancellationToken);

			var newPhoto = new Photo
			{
				UserId = request.UserId,
				Description = request.Description,
				Url = uploadResult.Url,
				ExternalId = uploadResult.PublicId,
			};
			user.AddPhoto(newPhoto);

			await _context.SaveChangesAsync(cancellationToken);

			return newPhoto.PhotoId;
		}
	}
}
