using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Utils;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Users.Queries.GetUserDetails
{
	public class UserDetailDto: IMapFrom<User>
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string PhotoUrl { get; set; }
		public ICollection<PhotoDto> Photos { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserDetailDto>()
				.ForMember(u => u.Age, options => options.MapFrom(u => u.DateOfBirth.CalculateAge()))
				.ForMember(u => u.PhotoUrl, options => options.MapFrom(u => u.GetMainPhotoUrl()));

		}
	}
}
