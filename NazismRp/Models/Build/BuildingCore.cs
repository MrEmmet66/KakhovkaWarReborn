using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazismRp.Models.Build
{
	internal class BuildingCore : IEntity
	{
		public int Id { get; set; }
		public PlayerModel Owner { get; set; }
		public List<PlayerModel> Members { get; set; }
		public int Health { get; set; }
		public int MaxHealth { get; set; }
		public List<BuildingObject> Objects { get; set; }
	}
}
