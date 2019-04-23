using System.Threading.Tasks;
using System.Linq;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Jelli.Data.Repositories
{
	public class GuildRoleRepository : IGuildRoleRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public GuildRoleRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<GuildRole> CreateGuildRoleAsync(GuildRole guildRole)
		{
			await _context.AddAsync(guildRole);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return guildRole;
			}
			return null;
		}

		public async Task<GuildRole> DeleteGuildRoleAsync(GuildRole guildRole)
		{
			_context.GuildRoles.Remove(guildRole);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return guildRole;
			}
			return null;
		}

		public async Task<IEnumerable<GuildRole>> GetGuildRolesAsync(ulong guildId)
		{
			return await _context.GuildRoles.Where(g => g.GuildId == guildId).ToListAsync();
		}

		public async Task<GuildRole> GetGuildRoleAsync(ulong guildId, string roleDisplayName)
		{
			return await _context.GuildRoles.Where(g => g.GuildId == guildId && g.RoleDisplayName.ToLower() == roleDisplayName.ToLower()).FirstOrDefaultAsync();
		}

		public async Task<GuildRole> UpdateGuildRoleAsync(GuildRole guildRole)
		{
			_context.GuildRoles.Update(guildRole);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return guildRole;
			}
			return null;
		}
		#endregion
	}
}