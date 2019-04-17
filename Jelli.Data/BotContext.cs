using System;
using Microsoft.EntityFrameworkCore;

namespace Jelli.Data
{
	public class BotContext : DbContext
	{
		#region Constructor
		public BotContext(DbContextOptions<BotContext> options)
				: base(options)
		{
		}
		#endregion

		#region Methods
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=jellibot.db");
		}
		#endregion
	}
}
