using System;
using System.Collections.Generic;
using System.Linq;
using FlirtingApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace FlirtingApp.Web.Identity
{
	public class User: IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		

		public DateTime DateOfBirth { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }

		private readonly HashSet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
		public IEnumerable<RefreshToken> RefreshTokens => _refreshTokens.ToList();

		// Set to public when seed photos
		private HashSet<Photo> _photos = new HashSet<Photo>();
		public IEnumerable<Photo> Photos => _photos.ToList();

		public void AddRefreshToken(string token, Guid userId, string remoteIpAddress, double daysToExpire = 5)
		{
			_refreshTokens.Add(new RefreshToken(token, userId, remoteIpAddress, DateTime.UtcNow.AddDays(daysToExpire)));
		}

		public void RemoveRefreshToken(string refreshToken)
		{
			_refreshTokens.Remove(RefreshTokens.First(t => t.Token == refreshToken));

		}

		public bool HasValidRefreshToken(string refreshToken)
		{
			return _refreshTokens.Any(t => t.Token == refreshToken && t.Active);
		}

		public Photo GetPhoto(Guid photoId)
		{
			return _photos.FirstOrDefault(p => p.Id == photoId);
		}

		public void AddPhoto(Photo photo)
		{
			if (!Photos.Any())
			{
				photo.IsMain = true;
			}

			_photos.Add(photo);
		}
	}
}
