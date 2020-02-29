using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FlirtingApp.Application.Common;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;
using FlirtingApp.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UploadResult = FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary.UploadResult;

namespace FlirtingApp.Infrastructure.ThirdPartyVendor
{
	class ImageHostAdapter: IImageHost
	{
		private readonly CloudinaryCredential _cloudinaryCredential;
		private readonly Cloudinary _cloudinary;
		private readonly Logger<ImageHostAdapter> _logger;
		public ImageHostAdapter(CloudinaryCredential cloudinaryCredential, Logger<ImageHostAdapter> logger)
		{
			_cloudinaryCredential = cloudinaryCredential;
			_logger = logger;
			var account = new Account(
				_cloudinaryCredential.CloudName,
				_cloudinaryCredential.ApiKey,
				_cloudinaryCredential.ApiSecret
			);
			_cloudinary = new Cloudinary(account);

		}

		public async Task<Result<UploadResult>> Upload(UploadOptions options, CancellationToken cancellationToken = default)
		{
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(options.FileName, options.FileStream),
				Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
			};
			try
			{
				var result = await _cloudinary.UploadAsync(uploadParams);
				return Result.Ok(new UploadResult
				{
					Url = result.Uri.ToString(),
					PublicId = result.PublicId
				});
			}
			catch (Exception e)
			{
				var message = "Upload image to cloudinary failed!";
				_logger.LogCritical(e, message);
				return Result.Fail<UploadResult>(message);
			}
			
		}
	}
}
