using Application;
using Application.Interfaces;
using Infraestructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.DependencyInjections
{
	public static class DependencyContainer
	{
		public static IServiceCollection BackendServices(this IServiceCollection services, ConnectionStringOptions options)
		{
			// Registrar servicios genéricos
        //services.AddScoped(typeof(IProcesResponse<>), typeof(ProcessResponse<>));

        // Registrar repositorios (Infrastructure)
        services.AddApplicationServices();

        // Registrar interactores (Application)
        services.AddInfrastructureServices(options);


			return services;
		}
	}
}
