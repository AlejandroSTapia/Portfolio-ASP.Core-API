﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Resources
	{
		public int ResourceId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
	}
}