using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IGuildRepository
	{
		Task<Guild> GetGuildAsync(ulong guildId);

		Task<Guild> CreateGuildAsync(Guild guild);

		Task<Guild> DeleteGuildAsync(ulong guildId);

		Task<Guild> UpdateGuildAsync(Guild guild);
	}
}