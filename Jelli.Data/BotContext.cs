using System;
using Jelli.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Jelli.Data
{
	public class BotContext : DbContext
	{
		#region Properties
		public DbSet<Guild> Guilds { get; set; }
		public DbSet<GuildRole> GuildRoles { get; set; }
		public DbSet<GuildUserNote> GuildUserNotes { get; set; }
		#endregion

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
