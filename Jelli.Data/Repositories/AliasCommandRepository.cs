using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Jelli.Data.Repositories
{
	public class AliasCommandRepository : IAliasCommandRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public AliasCommandRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<IEnumerable<AliasCommand>> GetAliasCommandsAsync(ulong guildId)
		{
			return await _context.AliasCommands.Where(g => g.GuildId == guildId).ToListAsync();
		}


		public async Task<AliasCommand> GetAliasCommandAsync(ulong guildId, string command)
		{
			return await _context.AliasCommands.FirstOrDefaultAsync(g => g.GuildId == guildId && g.Command == command);
		}

		public async Task<AliasCommand> CreateAliasCommandAsync(AliasCommand aliasCommand)
		{
			await _context.AddAsync(aliasCommand);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return aliasCommand;
			}
			return null;
		}

		public async Task<AliasCommand> DeleteAliasCommandAsync(AliasCommand aliasCommand)
		{
			_context.Remove(aliasCommand);
			var savedChanged = await _context.SaveChangesAsync();

			if (savedChanged > 0)
			{
				return aliasCommand;
			}
			return null;
		}

		public async Task<AliasCommand> UpdateAliasCommandAsync(AliasCommand aliasCommand)
		{
			_context.Update(aliasCommand);
			var savedChanged = await _context.SaveChangesAsync();

			if (savedChanged > 0)
			{
				return aliasCommand;
			}
			return null;
		}
		#endregion
	}
}