using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IGuildUserNoteRepository
	{
		Task<IEnumerable<GuildUserNote>> GetGuildUserNotesAsync(ulong guildId, ulong userId);

		Task<GuildUserNote> CreateGuildUserNoteAsync(GuildUserNote guildUserNote);

		Task<GuildUserNote> DeleteGuildUserNoteAsync(GuildUserNote guildUserNote);

		Task<GuildUserNote> UpdateGuildUserNoteAsync(GuildUserNote guildUserNote);
	}
}