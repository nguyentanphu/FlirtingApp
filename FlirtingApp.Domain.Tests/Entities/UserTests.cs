using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlirtingApp.Domain.Common;
using FlirtingApp.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace FlirtingApp.Domain.Tests.Entities
{
	public class UserTests
	{
		private readonly User _sut = new User(
			Guid.NewGuid(),
			"phunguyen",
			"phu",
			"nguyen",
			"nguyentanphu@hotmail.com",
			new DateTime(1992, 18, 5),
			Gender.Male,
			DateTime.UtcNow
		);

		[Fact]
		public void AddFirstPhoto_MarkAsMain()
		{
			var newPhoto = new Photo
			{
				//Id = Guid.NewGuid()
			};

			_sut.AddPhoto(newPhoto);

			_sut.Photos.Any(p => p.Id == newPhoto.Id && p.IsMain).Should().BeTrue();
		}

		[Fact]
		public void AddSecondPhoto_DoesNotMarkAsMain()
		{
			var firstPhoto = new Photo
			{
				//Id = Guid.NewGuid()
			};
			var secondPhoto = new Photo
			{
				//Id = Guid.NewGuid()
			};

			_sut.AddPhoto(firstPhoto);
			_sut.AddPhoto(secondPhoto);

			_sut.Photos.Any(p => p.Id == secondPhoto.Id && !p.IsMain).Should().BeTrue();
		}

		[Fact]
		public void GetMainPhoto_HaveNoPhotos_ReturnDefaultUrl()
		{
			var result = _sut.GetMainPhotoUrl();

			result.Should().Be("https://randomuser.me/api/portraits/lego/1.jpg");
		}

		[Fact]
		public void GetMainPhoto_ReturnUrlOfMainPhoto()
		{
			var newPhoto = new Photo
			{
				//Id = Guid.NewGuid(),
				Url = "test url"
			};

			_sut.AddPhoto(newPhoto);
			var result = _sut.GetMainPhotoUrl();

			result.Should().Be("test url");
		}
	}
}
