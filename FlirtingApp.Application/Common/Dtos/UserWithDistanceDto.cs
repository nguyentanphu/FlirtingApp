using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Users.Queries.GetUsers;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Common.Dtos
{
	public sealed class UserWithDistanceDto: User, IMapFrom<UserWithDistanceDto>
	{
		public double? Distance { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<UserWithDistanceDto, UserOverviewDto>();
		}
	}
}
