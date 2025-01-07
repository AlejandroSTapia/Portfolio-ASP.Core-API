using Application;
using Application.Interfaces;
using Infraestructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.DependencyInjections
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationDependencies(
			this IServiceCollection services, IConfiguration configuration)
		{
			// Cargar opciones de cadenas de conexión
			var connectionStringOptions = configuration
				.GetSection(ConnectionStringOptions.SectionKey)
				.Get<ConnectionStringOptions>();



			if (string.IsNullOrWhiteSpace(connectionStringOptions.ConnectionPortfolio))
			{
				throw new InvalidOperationException("The ConnectionPortfolio connection string is not configured.");
			}

			// Registrar dependencias de Infrastructure
			services.AddInfrastructureServices(connectionStringOptions);

			// Registrar dependencias de Application
			services.AddApplicationServices();

			return services;
		}
	}

}
