using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IGuildCustomCommandRepository
	{
		Task<IEnumerable<GuildCustomCommand>> GetGuildCustomCommandsAsync(ulong guildId);

		Task<GuildCustomCommand> CreateGuildCustomCommandAsync(GuildCustomCommand guildCustomCommand);

		Task<GuildCustomCommand> DeleteGuildCustomCommandAsync(GuildCustomCommand guildCustomCommand);

		Task<GuildCustomCommand> UpdateGuildCustomCommandAsync(GuildCustomCommand guildCustomCommand);
	}
}