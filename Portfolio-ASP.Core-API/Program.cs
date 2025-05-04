using Application;
using Infraestructure;
using Infraestructure.Configuration;
using Infraestructure.DependencyInjections;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Configuración cargada correctamente.");

builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Buscar archivos adicionales según disponibilidad
foreach (var env in new[] { "qa", "dev", "prod" })
{
	var fileName = $"appsettings.{env}.json";
	if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), fileName)))
	{
		builder.Configuration.AddJsonFile(fileName, optional: true, reloadOnChange: true);
		Console.WriteLine($"Cargando configuración desde: {fileName}");
		break; // Usa el primer archivo que encuentre
	}
}

builder.Services.AddApplicationDependencies(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API", Version = "v1" });
});


// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder
		.WithOrigins("https://mango-plant-0bb30020f.6.azurestaticapps.net")
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});

var app = builder.Build();

Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio V1"));
    app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler(errorApp =>
	{
		errorApp.Run(async context =>
		{
			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Ocurrió un error interno.");
		});
	});
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio V1"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
