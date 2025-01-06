using Application.Interfaces;
using Infraestructure.Configuration;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
	public static class DependencyContainer
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConnectionStringOptions options)
		{
			services.AddScoped<IResourcesRepository, ResourceRepository>();
			//services.AddDotNetCoreMicrosoftSQL(options.ConnectionPorfolio, "Portfolio");
			// o

			// Registrar la configuración individualmente
			services.Configure<ConnectionStringOptions>(config =>
			{
				config.ConnectionPorfolio = options.ConnectionPorfolio;
				// Configura otras propiedades de ConnectionStringOptions si las tienes
				config.MicrosoftEntraId = options.MicrosoftEntraId;
			});

			// Obtener la cadena de conexión y registrar el DbContext
			var connectionString = options.ConnectionPorfolio;
			services.AddDbContext<ApplicationDbContext>(dbOptions =>
				dbOptions.UseSqlServer(connectionString));

			return services;
		}
	}
}
