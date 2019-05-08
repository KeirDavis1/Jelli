using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IAliasCommandRepository
	{
		Task<IEnumerable<AliasCommand>> GetAliasCommandsAsync(ulong guildId);

		Task<AliasCommand> GetAliasCommandAsync(ulong guildId, string command);

		Task<AliasCommand> CreateAliasCommandAsync(AliasCommand aliasCommand);

		Task<AliasCommand> DeleteAliasCommandAsync(AliasCommand aliasCommand);

		Task<AliasCommand> UpdateAliasCommandAsync(AliasCommand aliasCommand);
	}
}