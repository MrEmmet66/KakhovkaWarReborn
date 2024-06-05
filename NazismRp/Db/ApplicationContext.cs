using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NazismRp.Db
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<PlayerModel> Players { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=players.db");
		}
	}
}
