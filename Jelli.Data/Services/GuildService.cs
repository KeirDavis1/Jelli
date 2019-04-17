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
		public Task<ServiceResponse<Guild>> DeleteGuildAsync(ulong guildId)
		{
			throw new System.NotImplementedException();
		}

		public async Task<ServiceResponse<Guild>> GetGuildAsync(ulong guildId)
		{
			var guild = await _guildRepository.GetGuildAsync(guildId); 
			
			if (guild != null)
			{
				return new ServiceResponse<Guild>(guild);
			}

			return new ServiceResponse<Guild>(null, success: false, message: "Failed to get the guild");
		}

		public async Task<ServiceResponse<Guild>> SetGuildCommandPrefixAsync(ulong guildId, string prefix)
		{
			var guild = await _guildRepository.GetGuildAsync(guildId);

			if (guild != null)
			{
				guild.CommandPrefix = prefix;
				var updated = await _guildRepository.UpdateGuildAsync(guild);
				if (updated != null)
				{
					return new ServiceResponse<Guild>(updated);
				}
				return new ServiceResponse<Guild>(null, success: false, message: "Failed to set the new prefix");
			}
			else
			{
				var newGuild = await _guildRepository.CreateGuildAsync(new Guild()
				{
					GuildId = guildId,
					CommandPrefix = prefix
				});
				if (newGuild != null)
				{
					return new ServiceResponse<Guild>(newGuild);
				}
				return new ServiceResponse<Guild>(null, success: false, message: "Failed to set the new prefix");
			}
		}
		#endregion
	}
}