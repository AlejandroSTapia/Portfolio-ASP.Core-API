using Domain.Entities;
using Infraestructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Infraestructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		private readonly ConnectionStringOptions ConnectionStringOptions;
		public ApplicationDbContext(IOptions<ConnectionStringOptions> options) 
		{
			ConnectionStringOptions = options.Value;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseSqlServer(ConnectionStringOptions.ConnectionPorfolio)
					.EnableDetailedErrors()
					.LogTo(message => Debug.WriteLine(message))
					.EnableSensitiveDataLogging()
					.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Resources>()
				.ToTable("Resources", "Porfolio")
				.HasKey(r => r.ResourceId);

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Resources> Resources { get; set; }
	}
	
	
}
