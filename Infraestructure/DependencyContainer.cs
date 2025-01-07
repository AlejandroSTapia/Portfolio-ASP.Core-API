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
		public static IServiceCollection AddInfrastructureServices(
			this IServiceCollection services, ConnectionStringOptions options)
		{
			// Registrar las opciones en el contenedor
			services.AddSingleton(options);

			services.AddScoped<IResourcesRepository, ResourceRepository>();
			//services.AddDotNetCoreMicrosoftSQL(options.ConnectionPorfolio, "Portfolio");
			// o

			// Registrar la configuración individualmente
			services.Configure<ConnectionStringOptions>(config =>
			{
				config.ConnectionPortfolio = options.ConnectionPortfolio;
				// Configura otras propiedades de ConnectionStringOptions si las tienes
				//config.MicrosoftEntraId = options.MicrosoftEntraId;
			});

			// Obtener la cadena de conexión y registrar el DbContext
			var connectionString = options.ConnectionPortfolio;
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new InvalidOperationException("The ConnectionPorfolio connection string is not configured.");
			}

			services.AddDbContext<ApplicationDbContext>(dbOptions =>
				dbOptions.UseSqlServer(options.ConnectionPortfolio)
				.EnableSensitiveDataLogging() // Muestra valores sensibles como la cadena de conexión.
			 .LogTo(Console.WriteLine)); 

			return services;
		}
	}
}
