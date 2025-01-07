using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio_ASP.Core_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ResourceController: ControllerBase
	{

		private readonly IResourcesService _resourcesService;

		public ResourceController(IResourcesService resourcesService)
		{
			_resourcesService = resourcesService;
		}

		[HttpGet]
		[Route("GetResources")]
		public async Task<IActionResult> GetAllResourcesAsync()
		{
			var resources = await _resourcesService.GetAllResourcesAsync();
			return Ok(resources);
		}
	}
}
