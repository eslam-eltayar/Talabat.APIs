﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class CustomerBasket
	{
        public string Id { get; set; }
		public List<BasketItme> Itmes { get; set; }
	}
}
