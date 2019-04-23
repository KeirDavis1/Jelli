using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IGuildRoleRepository
	{
		Task<IEnumerable<GuildRole>> GetGuildRolesAsync(ulong guildId);

		Task<GuildRole> CreateGuildRoleAsync(GuildRole guildRole);

		Task<GuildRole> DeleteGuildRoleAsync(GuildRole guildRole);

		Task<GuildRole> UpdateGuildRoleAsync(GuildRole guildRole);
	}
}