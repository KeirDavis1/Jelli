using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using Jelli.Data.Services.Communication;
using Jelli.Data.Services.Interfaces;

namespace Jelli.Data.Services
{
	public class GuildService : IGuildService
	{
		#region Properties
		private readonly IGuildRepository _guildRepository;
		#endregion

		#region Constructor
		public GuildService(IGuildRepository guildRepository)
		{
			_guildRepository = guildRepository;
		}
		#endregion

		#region Methods
		public Task<ServiceResponse<Guild>> CreateGuildAsync(Guild guild)
		{
			throw new System.NotImplementedException();
		}

		public Task<ServiceResponse<Guild>> DeleteGuildAsync(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public Task<ServiceResponse<IEnumerable<Guild>>> GetGuildAsync(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public Task<ServiceResponse<Guild>> UpdateGuildAsync(Guild guild)
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}