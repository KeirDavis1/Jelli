using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IAliasCommandRepository
	{
		Task<IEnumerable<AliasCommand>> GetAliasCommandsAsync(ulong guildId);

		Task<AliasCommand> GetAliasCommand(ulong guildId, string command);

		Task<AliasCommand> CreateGuildCustomCommandAsync(AliasCommand aliasCommand);

		Task<AliasCommand> DeleteGuildCustomCommandAsync(AliasCommand aliasCommand);

		Task<AliasCommand> UpdateGuildCustomCommandAsync(AliasCommand aliasCommand);
	}
}