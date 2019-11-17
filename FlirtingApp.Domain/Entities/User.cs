using System;
using System.Collections.Generic;
using System.Linq;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Domain.Entities
{
	public class User
	{
		public Guid UserId { get; set; }
		public Guid IdentityId { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }
		public string KnownAs { get; set; }
		public DateTime LastActive { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }


		// Set to public when seed photos
		private HashSet<Photo> _photos = new HashSet<Photo>();
		public IEnumerable<Photo> Photos => _photos.ToList();


		public Photo GetPhoto(Guid photoId)
		{
			return _photos.FirstOrDefault(p => p.PhotoId == photoId);
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
