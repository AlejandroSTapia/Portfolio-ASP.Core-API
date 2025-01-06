using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
	public class ResourcesService : IResourcesService
	{
		private readonly IResourcesRepository _resourcesRepository;

		public async Task<Resources> GetProductDetailsAsync(int id)
		{
			var resources = await _resourcesRepository.GetResourceByIdAsync(id);
			if (resources == null)
			{
				throw new Exception("Description not found");
			}
			return new Resources
			{
				ResourceId = resources.ResourceId,
				Name = resources.Name,
				Description = resources.Description
			};
		}
	}
}
