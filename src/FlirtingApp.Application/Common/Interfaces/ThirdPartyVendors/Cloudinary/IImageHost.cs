using System.Threading;
using System.Threading.Tasks;

namespace FlirtingApp.Application.Common.Interfaces.ThirdPartyVendors.Cloudinary
{
	public interface IImageHost
	{
		Task<Result<UploadResult>> Upload(UploadOptions options, CancellationToken cancellationToken = default);
	}
}
