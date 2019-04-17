using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Jelli.Data
{
	public class BotContextFactory : IDesignTimeDbContextFactory<BotContext>
	{
		public BotContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BotContext>();
			optionsBuilder.UseSqlite("Data Source=jellibot.db");

			return new BotContext(optionsBuilder.Options);
		}
	}
}