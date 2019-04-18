using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Services.Communication;

namespace Jelli.Data.Services.Interfaces
{
	public interface IGuildService
	{
		Task<ServiceResponse<Guild>> GetGuildAsync(ulong guildId);

		Task<ServiceResponse<Guild>> DeleteGuildAsync(ulong guildId);

		Task<ServiceResponse<string>> GetGuildCommandPrefixAsync(ulong guildId);

		Task<ServiceResponse<Guild>> SetGuildCommandPrefixAsync(ulong guildId, string prefix);
	}
}

