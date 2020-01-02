using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.Common.Interfaces.Databases;
using FlirtingApp.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FlirtingApp.Application.System.Commands.SeedData
{
	public class SeedDataCommand: IRequest
	{
	}

	public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
	{
		private readonly IIdentityDbContext _identityDbContext;
		private readonly IAppDbContext _dbContext;
		private readonly ISecurityUserManager _securityUserManager;
		private readonly IUserRepository _userRepo;
		private readonly IDbRunTimeConfig _dbRunTimeConfig;

		public SeedDataCommandHandler(
			IIdentityDbContext identityDbContext, 
			IAppDbContext dbContext,
			ISecurityUserManager securityUserManager, 
			IUserRepository userRepo, 
			IDbRunTimeConfig dbRunTimeConfig)
		{
			_identityDbContext = identityDbContext;
			_dbContext = dbContext;
			_securityUserManager = securityUserManager;
			_userRepo = userRepo;
			_dbRunTimeConfig = dbRunTimeConfig;
		}

		public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
		{
			await _identityDbContext.MigrateAsync(cancellationToken);
			await _dbContext.MigrateAsync(cancellationToken);

			if (await _userRepo.AnyAsync(u => true))
			{
				return Unit.Value;
			}

			await _dbRunTimeConfig.CreateLocationIndex();
			var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			var usersJson = File.ReadAllText(Path.Combine(dir, "System/Commands/SeedData/seedUserData.json"));
			var userList = JsonConvert.DeserializeObject<List<User>>(usersJson);

			var photosJson = File.ReadAllText(Path.Combine(dir, "System/Commands/SeedData/seedPhotoData.json"));
			var photoList = JsonConvert.DeserializeObject<List<Photo>>(photosJson);

			var locationsJson = File.ReadAllText(Path.Combine(dir, "System/Commands/SeedData/seedUserCoordinate.json"));
			locationsJson = locationsJson.Replace("\r\n\t", "");
			var coordinateList = JsonConvert.DeserializeObject<double[][]>(locationsJson);

			for (int i = 0; i < userList.Count; i++)
			{
				var currentUser = userList[i];
				var securityUserId = await _securityUserManager.CreateUserAsync(currentUser.UserName, "password");

				var property = typeof(User).GetProperty(nameof(User.IdentityId));
				property.GetSetMethod(true).Invoke(currentUser, new object[] { securityUserId });

				if (photoList.ElementAtOrDefault(i) != null)
				{
					currentUser.AddPhoto(photoList[i]);
				}
				currentUser.SetLocation(coordinateList[i]);

			}

			await _userRepo.AddRangeAsync(userList);

			return Unit.Value;
		}
	}
}
