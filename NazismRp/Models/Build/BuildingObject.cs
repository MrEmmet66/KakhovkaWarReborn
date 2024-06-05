using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazismRp.Models.Build
{
	internal class BuildingObject : IEntity
	{
		public int Id { get; set; }
		public int Health { get; set; }
		public int MaxHealth { get; set; }
		public int Level { get; set; }
	}
}
