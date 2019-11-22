using System.Threading;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary
{
	public interface ICloudinary
	{
		Task<CloudinaryUploadResult> Upload(CloudinaryUploadOptions options, CancellationToken cancellationToken = default);
	}
}
