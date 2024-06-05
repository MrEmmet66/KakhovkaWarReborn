using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.Entities.SAMP;

namespace NazismRp
{
	public class PlayerModel
	{
		public PlayerModel(string name, string passwordHash)
		{
			Name = name;
			PasswordHash = passwordHash;
		}
		// account data
		public int Id { get; set; }
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		
		//stats
		public int AdminLevel { get; set; } = 0;
		public int Skin { get; set; } = 127;
		public int WantedLevel { get; set; } = 0;
		public int Exp { get; set; } = 0;
		public int Level { get; set; } = 1;
		public int Money { get; set; } = 1488;
		public bool IsJew { get; set; }

		//spawn
		public float SpawnHealth { get; set; }
		public int SpawnInterior { get; set; }
		public int SpawnVirtualWorld { get; set; }
		public float SpawnX { get; set; }
		public float SpawnY { get; set; }
		public float SpawnZ { get; set; }
		
		// disconnect info
		public float Health { get; set; } = 100;
		public float Armor { get; set; } = 0;
		public int Interior { get; set; } = 0;
		public int VirtualWorld { get; set; } = 0;
		public float LastX { get; set; }
		public float LastY { get; set; }
		public float LastZ { get; set; }

		[NotMapped]
		public Vector3 LastPosition
		{
			get => new Vector3(LastX, LastY, LastZ);
			set
			{
				LastX = value.X;
				LastY = value.Y;
				LastZ = value.Z;
			}
		}

		[NotMapped]
		public Vector3 SpawnPosition
		{
			get => new Vector3(SpawnX, SpawnY, SpawnZ);
			set
			{
				SpawnX = value.X;
				SpawnY = value.Y;
				SpawnZ = value.Z;
			}
		}

	}
}
