using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlirtingApp.Application.Common.Interfaces
{
	public interface IAppDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Photo> Photos { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
		Task MigrateAsync(CancellationToken cancellationToken = default);
	}
}
