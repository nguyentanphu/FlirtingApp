using System.Linq;
using AutoMapper;
using FlirtingApp.Web.Dtos;
using FlirtingApp.Web.Identity;
using FlirtingApp.Web.Models;

namespace FlirtingApp.Web.Helpers
{
	public class AutoMapperProfiles: Profile
	{
		public AutoMapperProfiles()
		{
			//CreateMap<User, UserForListDto>()
			//	.ForMember(u => u.PhotoUrl,
			//		options => options.MapFrom((o, d) => o.Photos.FirstOrDefault()?.Url))
			//	.ForMember(u => u.Age, options =>
			//		options.MapFrom((o, d) => o.DateOfBirth.CalculateAge()));
			//CreateMap<User, UserDetail>()
			//	.ForMember(u => u.PhotoUrl,
			//		options => options.MapFrom((o, d) => o.Photos.FirstOrDefault()?.Url))
			//	.ForMember(u => u.Age, options =>
			//		options.MapFrom((o, d) => o.DateOfBirth.CalculateAge()));
			CreateMap<Photo, PhotoDto>();
			CreateMap<UserForUpdateDto, User>();
			CreateMap<PhotoForCreationDto, Photo>();
		}
	}
}
