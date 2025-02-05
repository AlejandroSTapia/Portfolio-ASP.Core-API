using Application;
using Infraestructure;
using Infraestructure.Configuration;
using Infraestructure.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDependencies(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder.AllowAnyOrigin()
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
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio V1"));

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
