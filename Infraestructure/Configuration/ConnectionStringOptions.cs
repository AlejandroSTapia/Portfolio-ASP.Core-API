using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configuration
{
	public class ConnectionStringOptions
	{
		public const string SectionKey = "ConnectionStrings";
		public string ConnectionPortfolio {  get; set; }
		public string MicrosoftEntraId { get; set; }
	}
}
