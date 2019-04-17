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
		public Guild CreateGuild(Guild guild)
		{
			throw new System.NotImplementedException();
		}

		public Guild DeleteGuild(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public Guild GetGuild(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public Guild UpdateGuild(Guild guild)
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}