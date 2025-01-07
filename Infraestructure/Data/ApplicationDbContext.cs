using Domain.Entities;
using Infraestructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Infraestructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Resources>()
				.ToTable("Resources", "Portf")
				.HasKey(r => r.ResourceId);

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Resources> Resources { get; set; }
	}
	
	
}
