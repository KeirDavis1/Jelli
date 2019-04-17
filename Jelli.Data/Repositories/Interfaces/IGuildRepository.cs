using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IGuildRepository
	{
		Guild GetGuild(ulong guildId);

		Guild CreateGuild(Guild guild);

		Guild DeleteGuild(ulong guildId);

		Guild UpdateGuild(Guild guild);
	}
}