using System.Threading.Tasks;
using System.Linq;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jelli.Data.Repositories
{
	public class GuildRepository : IGuildRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public GuildRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<Guild> CreateGuildAsync(Guild guild)
		{
			await _context.AddAsync(guild);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return guild;
			}
			return null;
		}

		public async Task<Guild> DeleteGuildAsync(Guild guild)
		{
			_context.Guilds.Remove(guild);
			var savedChanged = await _context.SaveChangesAsync();

			if (savedChanged > 0)
			{
				return guild;
			}
			return null;
		}

		public async Task<Guild> GetGuildAsync(ulong guildId)
		{
			return await _context.Guilds.FirstOrDefaultAsync(g => g.GuildId == guildId);
		}

		public async Task<Guild> UpdateGuildAsync(Guild guild)
		{
			_context.Guilds.Update(guild);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return guild;
			}
			return null;
		}
		#endregion
	}
}