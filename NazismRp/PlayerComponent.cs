using SampSharp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazismRp
{
	internal class PlayerComponent : Component
	{
		public PlayerModel Account { get; set; }
		public bool IsSpawned { get; set; }
        public bool IsLoggined { get; set; }
    }
}
