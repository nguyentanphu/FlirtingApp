using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.System.Commands.SeedData
{
	public class SeedDataCommand: IRequest
	{
	}

	public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
	{
		private readonly IAppIdentityDbContext _identityDbContext;
		private readonly IAppDbContext _dbContext;
		private readonly IAppUserManager _appUserManager;

		public SeedDataCommandHandler(
			IAppIdentityDbContext identityDbContext, 
			IAppDbContext dbContext, 
			IAppUserManager appUserManager
			)
		{
			_identityDbContext = identityDbContext;
			_dbContext = dbContext;
			_appUserManager = appUserManager;
		}

		public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
		{
			await _identityDbContext.MigrateAsync(cancellationToken);
			await _dbContext.MigrateAsync(cancellationToken);

			if (await _dbContext.Users.AnyAsync(cancellationToken))
			{
				return Unit.Value;
			}

			var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var jsonSerializeSettings = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			var usersJson = File.ReadAllText(Path.Combine(dir, "System/Commands/SeedData/seedUserData.json"));
			var userList = JsonSerializer.Deserialize<List<User>>(usersJson, jsonSerializeSettings);

			var photosJson = File.ReadAllText(Path.Combine(dir, "System/Commands/SeedData/seedPhotoData.json"));
			var photoList = JsonSerializer.Deserialize<List<Photo>>(photosJson, jsonSerializeSettings);

			for (int i = 0; i < userList.Count; i++)
			{
				var currentUser = userList[i];
				var appUserId = await _appUserManager.CreateUserAsync(currentUser.UserName, "password");
				currentUser.IdentityId = appUserId;
				if (photoList.ElementAtOrDefault(i) != null)
				{
					currentUser.AddPhoto(photoList[i]);
				}

				_dbContext.Users.Add(currentUser);
			}

			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
