using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;
using FlirtingApp.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Options;

namespace FlirtingApp.Infrastructure.ThirdPartyVendor
{
	class CloudinaryAdapter: ICloudinary
	{
		private readonly CloudinaryCredential _cloudinaryCredential;
		private readonly Cloudinary _cloudinary;
		public CloudinaryAdapter(IOptions<CloudinaryCredential> cloudinaryCredential)
		{
			_cloudinaryCredential = cloudinaryCredential.Value;
			var account = new Account(
				_cloudinaryCredential.CloudName,
				_cloudinaryCredential.ApiKey,
				_cloudinaryCredential.ApiSecret
			);
			_cloudinary = new Cloudinary(account);

		}

		public async Task<CloudinaryUploadResult> Upload(CloudinaryUploadOptions options, CancellationToken cancellationToken = default)
		{
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(options.FileName, options.FileStream),
				Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
			};
			var result = await _cloudinary.UploadAsync(uploadParams);
			return new CloudinaryUploadResult
			{
				Url = result.Uri.ToString(),
				PublicId = result.PublicId
			};
		}
	}
}
