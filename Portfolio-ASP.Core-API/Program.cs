using Application;
using Infraestructure;
using Infraestructure.Configuration;
using Infraestructure.DependencyInjections;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Configuración cargada correctamente.");
Console.WriteLine($"Environment detectado: {builder.Environment.EnvironmentName}");

// 🔹 Dependencias
builder.Services.AddApplicationDependencies(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
});

// 🔹 Configuración de CORS condicional
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		if (builder.Environment.IsDevelopment())
		{
			// En local -> cualquier origen
			policy.AllowAnyOrigin()
				  .AllowAnyMethod()
				  .AllowAnyHeader();
		}
		else
		{
			// En QA/Prod -> solo dominios permitidos
			policy.WithOrigins(
					"https://mango-plant-0bb30020f.6.azurestaticapps.net"
				)
				.AllowAnyMethod()
				.AllowAnyHeader();
		}
	});
});

var app = builder.Build();

Console.WriteLine($"Environment (app): {app.Environment.EnvironmentName}");

// 🔹 Configuración de middlewares
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseExceptionHandler(errorApp =>
	{
		errorApp.Run(async context =>
		{
			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Ocurrió un error interno.");
		});
	});

	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio V1"));
}

app.UseHttpsRedirection();

app.UseRouting();

// 🔹 CORS SIEMPRE antes de Auth y Controllers
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();