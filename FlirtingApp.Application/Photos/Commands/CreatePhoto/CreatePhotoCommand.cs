using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Entities;
using MediatR;

namespace FlirtingApp.Application.Photos.Commands.CreatePhoto
{
	public class CreatePhotoCommand: CreatePhotoRequest, IRequest<Guid>
	{
		public Guid UserId { get; set; }
	}

	public class CreatePhotoCommandHandler: IRequestHandler<CreatePhotoCommand, Guid>
	{
		private readonly ICloudinary _cloudinary;
		private readonly IUserRepository _userRepository;
		public CreatePhotoCommandHandler(ICloudinary cloudinary, IUserRepository userRepository)
		{
			_cloudinary = cloudinary;
			_userRepository = userRepository;
		}

		public async Task<Guid> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetAsync(request.UserId);
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

			await _userRepository.UpdateAsync(user);

			return newPhoto.Id;
		}
	}
}
