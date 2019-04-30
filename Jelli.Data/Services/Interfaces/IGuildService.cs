using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Services.Communication;
using Jelli.Data.Types;

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

		Task<ServiceResponse<GuildCustomCommand>> GetGuildCustomCommandByCommandAsync(ulong guildId, string command);

		Task<ServiceResponse<GuildCustomCommand>> CreateGuildCustomCommandAsync(ulong guildId, string command, string response);

		Task<ServiceResponse<ChannelEnforcement>> GetChannelEnforcementAsync(ulong guildId, ulong channelId);

		Task<ServiceResponse<ChannelEnforcement>> CreateChannelEnforcementAsync(ulong guildId, ulong channelId);

		Task<ServiceResponse<ChannelEnforcement>> ConfigureChannelEnforcementAsync(ulong guildId, ulong channelId, EEnforcementType type, string value);

		Task<ServiceResponse<ChannelEnforcement>> DeleteChannelEnforcementAsync(ulong guildId, ulong channelId);
	}
}

