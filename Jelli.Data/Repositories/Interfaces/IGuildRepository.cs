using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IGuildRepository
	{
		Task<Guild> GetGuild(ulong guildId);

		Task<Guild> CreateGuild(Guild guild);

		Task<Guild> DeleteGuild(ulong guildId);

		Task<Guild> UpdateGuild(Guild guild);
	}
}