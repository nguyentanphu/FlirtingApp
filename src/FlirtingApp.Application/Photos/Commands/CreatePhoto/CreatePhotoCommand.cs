using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;
using FlirtingApp.Application.Exceptions;
using FlirtingApp.Domain.Entities;
using MediatR;

namespace FlirtingApp.Application.Photos.Commands.CreatePhoto
{
	public class CreatePhotoCommand: CreatePhotoRequest, IRequest<Result<Guid>>
	{
		public Guid UserId { get; set; }
	}

	public class CreatePhotoCommandHandler: IRequestHandler<CreatePhotoCommand, Result<Guid>>
	{
		private readonly IImageHost _imageHost;
		private readonly IUserRepository _userRepository;
		public CreatePhotoCommandHandler(IImageHost imageHost, IUserRepository userRepository)
		{
			_imageHost = imageHost;
			_userRepository = userRepository;
		}

		public async Task<Result<Guid>> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
		{
			var user = await _userRepository.GetAsync(request.UserId);
			if (user == null)
			{
				throw new ResourceNotFoundException("User", request.UserId);
			}

			using var fileStream = request.File.OpenReadStream();
			var uploadOptions = new UploadOptions
			{
				FileName = request.File.FileName,
				FileStream = fileStream
			};
			var uploadResult = await _imageHost.Upload(uploadOptions, cancellationToken);

			if (uploadResult.Failure)
			{
				return Result.Fail<Guid>(uploadResult.Error);
			}

			var newPhoto = new Photo(
				uploadResult.Value.Url,
				uploadResult.Value.PublicId,
				request.Description
			);
			user.AddPhoto(newPhoto);

			await _userRepository.UpdateAsync(user);

			return Result.Ok(newPhoto.Id);
		}
	}
}
