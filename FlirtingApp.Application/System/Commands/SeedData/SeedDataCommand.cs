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
using MongoDB.Driver;

namespace FlirtingApp.Application.System.Commands.SeedData
{
	public class SeedDataCommand: IRequest
	{
	}

	public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
	{
		private readonly IIdentityDbContext _identityDbContext;
		private readonly IAppDbContext _dbContext;
		private readonly IMongoClient _mongoClient;
		private readonly ISecurityUserManager _securityUserManager;
		private readonly IUserRepository _userRepo;

		public SeedDataCommandHandler(
			IIdentityDbContext identityDbContext, 
			IAppDbContext dbContext,
			IMongoClient mongoClient,
			ISecurityUserManager securityUserManager, 
			IUserRepository userRepo
			)
		{
			_identityDbContext = identityDbContext;
			_dbContext = dbContext;
			_securityUserManager = securityUserManager;
			_userRepo = userRepo;
			_mongoClient = mongoClient;
		}

		public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
		{
			await _identityDbContext.MigrateAsync(cancellationToken);
			await _dbContext.MigrateAsync(cancellationToken);

			if (await _dbContext.Users.AnyAsync(cancellationToken) && await _userRepo.AnyAsync(u => true))
			{
				return Unit.Value;
			}

			await RecreateDatabase(cancellationToken);

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
				var securityUserId = await _securityUserManager.CreateUserAsync(currentUser.UserName, "password");
				currentUser.IdentityId = securityUserId;
				if (photoList.ElementAtOrDefault(i) != null)
				{
					currentUser.AddPhoto(photoList[i]);
				}

			}

			await _userRepo.AddRangeAsync(userList);

			return Unit.Value;
		}

		private async Task RecreateDatabase(CancellationToken cancellationToken = default)
		{
			await _dbContext.EnsureDeletedAsync(cancellationToken);
			await _mongoClient.DropDatabaseAsync("FlirtingApp", cancellationToken);
			await _identityDbContext.MigrateAsync(cancellationToken);
			await _dbContext.MigrateAsync(cancellationToken);
		}
	}
}
