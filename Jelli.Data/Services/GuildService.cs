using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using Jelli.Data.Services.Communication;
using Jelli.Data.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Jelli.Data.Services
{
	public class GuildService : IGuildService
	{
		#region Properties
		private readonly IGuildRepository _guildRepository;
		private readonly IMemoryCache _memoryCache;
		#endregion

		#region Constructor
		public GuildService(IGuildRepository guildRepository, IMemoryCache memoryCache)
		{
			_guildRepository = guildRepository;
			_memoryCache = memoryCache;
		}
		#endregion

		#region Methods
		public string GetPrefixCacheKeyName(ulong guildId)
		{
			return $"g_{guildId}_prefix";
		}

		public TimeSpan GetPrefixCacheExpiryTime()
		{
			return new TimeSpan(hours: 0, minutes: 30, seconds: 0);
		}

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

		public async Task<ServiceResponse<string>> GetGuildCommandPrefixAsync(ulong guildId)
		{
			string guildPrefix;
			// Attempt to get the prefix
			if (_memoryCache.TryGetValue(GetPrefixCacheKeyName(guildId), out guildPrefix))
			{
				return new ServiceResponse<string>(guildPrefix);
			}

			// Use the database as it's not in memory
			var dbGuild = await _guildRepository.GetGuildAsync(guildId);

			if (dbGuild != null)
			{
				// Set the prefix into cache
				_memoryCache.Set(GetPrefixCacheKeyName(guildId), dbGuild.CommandPrefix, GetPrefixCacheExpiryTime());
				// Return the database command prefix to the user
				return new ServiceResponse<string>(dbGuild.CommandPrefix);
			}

			return null;
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
					// Set the prefix into cache
					_memoryCache.Set(GetPrefixCacheKeyName(guildId), prefix, GetPrefixCacheExpiryTime());
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
					// Set the prefix into cache
					_memoryCache.Set(GetPrefixCacheKeyName(guildId), prefix, GetPrefixCacheExpiryTime());
					// Return the guild
					return new ServiceResponse<Guild>(newGuild);
				}
				return new ServiceResponse<Guild>(null, success: false, message: "Failed to set the new prefix");
			}
		}
		#endregion
	}
}