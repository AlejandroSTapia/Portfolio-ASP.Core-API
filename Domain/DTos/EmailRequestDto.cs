using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTos
{
	public class EmailRequestDto
	{
		public string Name { get; set; } = "";
		public string Email { get; set; } = "";
		public string Message { get; set; } = "";
	}
}
