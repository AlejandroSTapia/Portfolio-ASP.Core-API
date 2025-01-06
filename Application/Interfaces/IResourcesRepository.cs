using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IResourcesRepository
	{
		Task<IEnumerable<Resources>> GetAllResourcesAsync();
		Task<Resources> GetResourceByIdAsync(int resourceId);
	}
}
