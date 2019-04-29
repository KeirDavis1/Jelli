using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Jelli.Data.Repositories
{
	public class GuildCustomCommandRepository : IGuildCustomCommandRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public GuildCustomCommandRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<GuildCustomCommand> CreateGuildCustomCommandAsync(GuildCustomCommand guildCustomCommand)
		{
			await _context.AddAsync(guildCustomCommand);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return guildCustomCommand;
			}
			return null;
		}

		public async Task<GuildCustomCommand> DeleteGuildCustomCommandAsync(GuildCustomCommand guildCustomCommand)
		{
			_context.Remove(guildCustomCommand);
			var savedChanged = await _context.SaveChangesAsync();

			if (savedChanged > 0)
			{
				return guildCustomCommand;
			}
			return null;
		}

		public async Task<IEnumerable<GuildCustomCommand>> GetGuildCustomCommandsAsync(ulong guildId)
		{
			return await _context.GuildCustomCommands.Where(a => a.GuildId == guildId).ToArrayAsync();
		}

		public Task<GuildCustomCommand> UpdateGuildCustomCommandAsync(GuildCustomCommand guildCustomCommand)
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}