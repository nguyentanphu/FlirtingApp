using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary;

namespace FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors
{
	public interface ICloudinary
	{
		Task<CloudinaryUploadResult> Upload(CloudinaryUploadOptions options, CancellationToken cancellationToken = default);
	}
}
