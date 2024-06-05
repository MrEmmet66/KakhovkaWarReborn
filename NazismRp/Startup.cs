using Microsoft.Extensions.DependencyInjection;
using NazismRp.Db;
using NazismRp.Repositories;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace NazismRp
{
	public class Startup : IStartup
	{
		public void Configure(IServiceCollection services)
		{
			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddDbContext<ApplicationContext>();

			services.AddSystemsInAssembly();
		}

		public void Configure(IEcsBuilder builder)
		{
			// TODO: Enable desired ECS system features
			builder.EnableSampEvents()
				.EnablePlayerCommands()
				.EnableRconCommands();
		}
	}
}