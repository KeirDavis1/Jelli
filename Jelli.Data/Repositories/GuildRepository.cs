using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;

namespace Jelli.Data.Repositories
{
	public class GuildRepository : IGuildRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public GuildRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public Task<Guild> CreateGuildAsync(Guild guild)
		{
			throw new System.NotImplementedException();
		}

		public Task<Guild> DeleteGuildAsync(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public Task<Guild> GetGuildAsync(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public Task<Guild> UpdateGuildAsync(Guild guild)
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}