using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Jelli.Data.Repositories
{
	public class GuildUserNoteRepository : IGuildUserNoteRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public GuildUserNoteRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<GuildUserNote> CreateGuildUserNoteAsync(GuildUserNote guildUserNote)
		{
			await _context.AddAsync(guildUserNote);
			var changes = await _context.SaveChangesAsync();

			if (changes > 0)
			{
				return guildUserNote;
			}
			return null;
		}

		public Task<GuildUserNote> DeleteGuildUserNoteAsync(GuildUserNote guildUserNote)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<GuildUserNote>> GetGuildUserNotesAsync(ulong guildId, ulong userId)
		{
			return await _context.GuildUserNotes.Where(a => a.GuildId == guildId && a.UserId == userId).ToListAsync();
		}

		public Task<GuildUserNote> UpdateGuildUserNoteAsync(GuildUserNote guildUserNote)
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}