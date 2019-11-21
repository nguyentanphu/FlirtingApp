using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Utils;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Users.Queries.GetUsers
{
	public class GetUsersQueryResponse
	{
		public IEnumerable<UserOverviewDto> UserOverviewDtos { get; set; }
	}
}
