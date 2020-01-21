using System;
using AutoMapper;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Domain.Entities;

namespace FlirtingApp.Application.Photos
{
	public class PhotoDto : IMapFrom<Photo>
	{
		public Guid Id { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public DateTime DateAdded { get; set; }
		public bool IsMain { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Photo, PhotoDto>()
				.ForMember(p => p.DateAdded, options => options.MapFrom(p => p.Created));
		}
	}
}
