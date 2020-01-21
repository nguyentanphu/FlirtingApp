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
			new DateTime(1992, 5, 18),
			Gender.Male,
			DateTime.UtcNow
		);

		[Fact]
		public void AddFirstPhoto_MarkAsMain()
		{
			var newPhoto = new Photo("http://abc", "545uuhh", "nod dsessd");

			_sut.AddPhoto(newPhoto);

			_sut.Photos.Any(p => p.Id == newPhoto.Id && p.IsMain).Should().BeTrue();
		}

		[Fact]
		public void AddSecondPhoto_DoesNotMarkAsMain()
		{
			var firstPhoto = new Photo("http://abc", "545uuhh", "nod dsessd");
			var secondPhoto = new Photo("http://abc35544", "secondId", "nod dsessd");

			_sut.AddPhoto(firstPhoto);
			_sut.AddPhoto(secondPhoto);

			_sut.Photos.Any(p => p.IsMain).Should().BeTrue();
			_sut.Photos.Any(p => !p.IsMain).Should().BeTrue();
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
			var newPhoto = new Photo("http://testurl", "545uuhh", "nod dsessd");

			_sut.AddPhoto(newPhoto);
			var result = _sut.GetMainPhotoUrl();

			result.Should().Be("http://testurl");
		}
	}
}
