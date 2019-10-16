using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlirtingApp.Api.Dtos;
using FlirtingApp.Api.Helpers.Extensions;
using FlirtingApp.Api.Identity;
using FlirtingApp.Api.Models;

namespace FlirtingApp.Api.Helpers
{
	public class AutoMapperProfiles: Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, UserForListDto>()
				.ForMember(u => u.PhotoUrl,
					options => options.MapFrom((o, d) => o.Photos.FirstOrDefault()?.Url))
				.ForMember(u => u.Age, options =>
					options.MapFrom((o, d) => o.DateOfBirth.CalculateAge()));
			CreateMap<User, UserDetail>()
				.ForMember(u => u.PhotoUrl,
					options => options.MapFrom((o, d) => o.Photos.FirstOrDefault()?.Url))
				.ForMember(u => u.Age, options =>
					options.MapFrom((o, d) => o.DateOfBirth.CalculateAge()));
			CreateMap<Photo, PhotoDto>();
		}
	}
}
