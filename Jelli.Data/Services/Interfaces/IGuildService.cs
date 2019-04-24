using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Services.Communication;

namespace Jelli.Data.Services.Interfaces
{
	public interface IGuildService
	{
		Task<ServiceResponse<Guild>> GetGuildAsync(ulong guildId);

		Task<ServiceResponse<Guild>> CreateGuildAsync(ulong guildId);

		Task<ServiceResponse<Guild>> DeleteGuildAsync(ulong guildId);

		Task<ServiceResponse<string>> GetGuildCommandPrefixAsync(ulong guildId);

		Task<ServiceResponse<Guild>> SetGuildCommandPrefixAsync(ulong guildId, string prefix);

		Task<ServiceResponse<GuildRole>> CreateGuildRoleAsync(ulong guildId, ulong roleId, string roleDisplayName);

		Task<ServiceResponse<IEnumerable<GuildRole>>> GetGuildRolesAsync(ulong guildId);

		Task<ServiceResponse<GuildRole>> GetGuildRoleAsync(ulong guildId, string roleDisplayName);

		Task<ServiceResponse<IEnumerable<GuildUserNote>>> GetGuildUserNotesAsync(ulong guildId, ulong userId);

		Task<ServiceResponse<GuildUserNote>> CreateGuildUserNoteAsync(ulong guildId, ulong userId, ulong submitterId, string content);

	}
}

