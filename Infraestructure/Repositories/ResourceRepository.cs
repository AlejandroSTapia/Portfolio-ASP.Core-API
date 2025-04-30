using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
	public class ResourceRepository : IResourcesRepository
	{
		private readonly ApplicationDbContext _context;

		public ResourceRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Resources>> GetAllResourcesAsync()
		{
			return await _context.Resources.ToListAsync();
		}

		public async Task<Resources> GetResourceByIdAsync(int id)
		{
			return await _context.Resources.FindAsync(id);
		}

		public async Task<string?> GetValueAsync(string name)
		{
			return await _context.Resources
				.Where(r => r.Name == name)
				.Select(r => r.Value)
				.FirstOrDefaultAsync();
		}
	}
}
