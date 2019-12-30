using System;
using System.Collections.Generic;
using System.Linq;
using FlirtingApp.Domain.Common;

namespace FlirtingApp.Domain.Entities
{
	public class User: AuditableEntity
	{
		private User() { }

		public User(
			Guid securityUserId,
			string userName,
			string firstName,
			string lastName,
			string email,
			DateTime dateOfBirth,
			Gender gender,
			DateTime machineUtcNow
			)
		{
			IdentityId = securityUserId;
			UserName = userName;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			DateOfBirth = dateOfBirth;
			Gender = gender;
			LastActive = machineUtcNow;
		}
		public Guid IdentityId { get; private set; }

		public string UserName { get; private set; }
		public string Email { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }

		public Gender Gender { get; private set; }

		public DateTime DateOfBirth { get; private set; }
		public DateTime LastActive { get; private set; }

		public void UpdateLastActive(DateTime machineUtcNow)
		{
			LastActive = machineUtcNow;
		}
		public string KnownAs { get; private set; }
		
		public string Introduction { get; private set; }
		public string LookingFor { get; private set; }
		public string Interests { get; private set; }
		public string City { get; private set; }
		public string Country { get; private set; }

		private ICollection<Photo> _photos = new List<Photo>();
		public IEnumerable<Photo> Photos => _photos.ToArray();

		public void UpdateUserAdditionalInfo(
			string knownAs,
			string introduction,
			string lookingFor,
			string interests,
			string city,
			string country
			)
		{
			KnownAs = knownAs;
			Introduction = introduction;
			LookingFor = lookingFor;
			Interests = interests;
			City = city;
			Country = country;
		}

		public Photo GetPhoto(Guid photoId)
		{
			return _photos.FirstOrDefault(p => p.Id == photoId);
		}

		public string GetMainPhotoUrl()
		{
			return _photos.FirstOrDefault(p => p.IsMain)?.Url ?? "https://randomuser.me/api/portraits/lego/1.jpg";
		}

		public void AddPhoto(Photo photo)
		{
			if (!Photos.Any())
			{
				photo.SetMain();
			}

			_photos.Add(photo);
		}

		
	}
}
