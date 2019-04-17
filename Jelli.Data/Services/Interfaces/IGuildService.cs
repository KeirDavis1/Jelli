using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Services.Communication;

namespace Jelli.Data.Services.Interfaces
{
	public interface IGuildService
	{
		Task<ServiceResponse<IEnumerable<Guild>>> GetGuildAsync(ulong guildId);

		Task<ServiceResponse<Guild>> CreateGuildAsync(Guild guild);

		Task<ServiceResponse<Guild>> DeleteGuildAsync(ulong guildId);

		Task<ServiceResponse<Guild>> UpdateGuildAsync(Guild guild);
	}
}

