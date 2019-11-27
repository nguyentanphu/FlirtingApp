using System;
using System.Threading;
using System.Threading.Tasks;
using FlirtingApp.Application.System.Commands.SeedData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlirtingApp.WebApi.HostedServices
{
	public class MigrationHostedService: IHostedService
	{
		private readonly IServiceProvider _serviceProvider;

		public MigrationHostedService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			
			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			await mediator.Send(new SeedDataCommand(), cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
