using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using Jelli.Data.Services.Communication;
using Jelli.Data.Services.Interfaces;
using Jelli.Data.Types;
using Microsoft.Extensions.Caching.Memory;

namespace Jelli.Data.Services
{
	public class GuildService : IGuildService
	{
		#region Properties
		private readonly IGuildRepository _guildRepository;
		private readonly IGuildRoleRepository _guildRoleRepository;
		private readonly IGuildUserNoteRepository _guildUserNoteRepository;
		private readonly IGuildCustomCommandRepository _guildCustomCommandRepository;
		private readonly IChannelEnforcementRepository _channelEnforcementRepository;
		private readonly IAliasCommandRepository _aliasCommandRepository;
		private readonly IMemoryCache _memoryCache;
		#endregion

		#region Constructor
		public GuildService(IGuildRepository guildRepository, IGuildRoleRepository guildRoleRepository, IGuildUserNoteRepository guildUserNoteRepository, IGuildCustomCommandRepository guildCustomCommandRepository, IChannelEnforcementRepository channelEnforcementRepository, IAliasCommandRepository aliasCommandRepository, IMemoryCache memoryCache)
		{
			_guildRepository = guildRepository;
			_guildRoleRepository = guildRoleRepository;
			_guildUserNoteRepository = guildUserNoteRepository;
			_guildCustomCommandRepository = guildCustomCommandRepository;
			_channelEnforcementRepository = channelEnforcementRepository;
			_aliasCommandRepository = aliasCommandRepository;
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

		public async Task<ServiceResponse<Guild>> DeleteGuildAsync(ulong guildId)
		{
			var guild = await _guildRepository.GetGuildAsync(guildId);
			if (guild != null)
			{
				// Call the repository to delete the guild
				var deletedGuild = _guildRepository.DeleteGuildAsync(guild);
				if (deletedGuild != null)
				{
					// Deleted the guild
					return new ServiceResponse<Guild>(guild);
				}
				// Deleting the guild failed
				return new ServiceResponse<Guild>(null, success: false, message: "Failed to delete the guild");
			}
			// Couldn't get the guild
			return new ServiceResponse<Guild>(null, success: false, message: "Failed to get the guild");
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

			return new ServiceResponse<string>(null, success: false, message: "Guild not registered in database");
		}

		public async Task<ServiceResponse<Guild>> CreateGuildAsync(ulong guildId)
		{
			var guild = await _guildRepository.CreateGuildAsync(new Guild
			{
				GuildId = guildId
			});

			if (guild != null)
			{
				return new ServiceResponse<Guild>(guild);
			}
			return new ServiceResponse<Guild>(null, success: false, message: "Could not create guild");
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

		public async Task<ServiceResponse<GuildRole>> CreateGuildRoleAsync(ulong guildId, ulong roleId, string roleDisplayName)
		{
			if (_guildRepository.GetGuildAsync(guildId) == null)
			{
				var guildDb = await _guildRepository.CreateGuildAsync(new Guild
				{
					GuildId = guildId
				});
				if (guildDb == null)
				{
					return new ServiceResponse<GuildRole>(null, success: false, message: "Couldn't create guild");
				}
			}

			var newGuildRole = new GuildRole
			{
				GuildId = guildId,
				RoleId = roleId,
				RoleDisplayName = roleDisplayName
			};
			var createdGuildRole = await _guildRoleRepository.CreateGuildRoleAsync(newGuildRole);

			if (createdGuildRole != null)
			{
				return new ServiceResponse<GuildRole>(createdGuildRole);
			}
			return new ServiceResponse<GuildRole>(null, success: false, message: "Failed to create guild role");
		}

		public async Task<ServiceResponse<IEnumerable<GuildRole>>> GetGuildRolesAsync(ulong guildId)
		{
			var dbRoles = await _guildRoleRepository.GetGuildRolesAsync(guildId);
			return new ServiceResponse<IEnumerable<GuildRole>>(dbRoles);
		}

		public async Task<ServiceResponse<GuildRole>> GetGuildRoleAsync(ulong guildId, string roleDisplayName)
		{
			var dbRole = await _guildRoleRepository.GetGuildRoleAsync(guildId, roleDisplayName);
			if (dbRole != null)
			{
				return new ServiceResponse<GuildRole>(dbRole);
			}
			return new ServiceResponse<GuildRole>(null, success: false, message: "Failed to find the guild role");
		}

		public async Task<ServiceResponse<IEnumerable<GuildUserNote>>> GetGuildUserNotesAsync(ulong guildId, ulong userId)
		{
			var dbResponse = await _guildUserNoteRepository.GetGuildUserNotesAsync(guildId, userId);
			if (dbResponse != null)
			{
				return new ServiceResponse<IEnumerable<GuildUserNote>>(dbResponse);
			}
			return new ServiceResponse<IEnumerable<GuildUserNote>>(null, success: false, message: "Failed to get notes for user");
		}

		public async Task<ServiceResponse<GuildUserNote>> CreateGuildUserNoteAsync(ulong guildId, ulong userId, ulong submitterId, string content)
		{
			if (await _guildRepository.GetGuildAsync(guildId) == null)
			{
				var guildDb = await _guildRepository.CreateGuildAsync(new Guild
				{
					GuildId = guildId
				});
				if (guildDb == null)
				{
					return new ServiceResponse<GuildUserNote>(null, success: false, message: "Couldn't create guild");
				}
			}

			var dbResponse = await _guildUserNoteRepository.CreateGuildUserNoteAsync(new GuildUserNote
			{
				GuildId = guildId,
				UserId = userId,
				SubmitterId = submitterId,
				Content = content,
				Created = DateTime.UtcNow
			});
			if (dbResponse != null)
			{
				return new ServiceResponse<GuildUserNote>(dbResponse);
			}
			return new ServiceResponse<GuildUserNote>(null, success: false, message: "Failed to get create the note for this user");
		}

		public async Task<ServiceResponse<GuildCustomCommand>> GetGuildCustomCommandByCommandAsync(ulong guildId, string command)
		{
			var commands = await _guildCustomCommandRepository.GetGuildCustomCommandsAsync(guildId);
			foreach (var dbCommand in commands)
			{
				if (dbCommand.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase))
				{
					return new ServiceResponse<GuildCustomCommand>(dbCommand);
				}
			}
			return new ServiceResponse<GuildCustomCommand>(null, success: false, message: "Could not find command for this guild");
		}

		public async Task<ServiceResponse<GuildCustomCommand>> CreateGuildCustomCommandAsync(ulong guildId, string command, string response)
		{
			if (await _guildRepository.GetGuildAsync(guildId) == null)
			{
				var guildDb = await _guildRepository.CreateGuildAsync(new Guild
				{
					GuildId = guildId
				});
				if (guildDb == null)
				{
					return new ServiceResponse<GuildCustomCommand>(null, success: false, message: "Couldn't create guild");
				}
			}

			var dbResponse = await _guildCustomCommandRepository.CreateGuildCustomCommandAsync(new GuildCustomCommand
			{
				GuildId = guildId,
				Command = command,
				Response = response
			});
			if (dbResponse != null)
			{
				return new ServiceResponse<GuildCustomCommand>(dbResponse);
			}
			return new ServiceResponse<GuildCustomCommand>(null, success: false, message: "Failed to get create the custom command for this guild");
		}

		public async Task<ServiceResponse<ChannelEnforcement>> GetChannelEnforcementAsync(ulong guildId, ulong channelId)
		{
			if (await _guildRepository.GetGuildAsync(guildId) == null)
			{
				var guildDb = await _guildRepository.CreateGuildAsync(new Guild
				{
					GuildId = guildId
				});
				if (guildDb == null)
				{
					return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Couldn't create guild");
				}
			}

			var dbResponse = await _channelEnforcementRepository.GetChannelEnforcementAsync(guildId, channelId);
			if (dbResponse != null)
			{
				return new ServiceResponse<ChannelEnforcement>(dbResponse);
			}
			return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Failed to get the channel enforcement");
		}

		public async Task<ServiceResponse<ChannelEnforcement>> CreateChannelEnforcementAsync(ulong guildId, ulong channelId)
		{
			var existingEnforcement = await GetChannelEnforcementAsync(guildId, channelId);
			if (existingEnforcement?.ServiceObject != null)
			{
				return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Channel enforcement is already setup");
			}

			var dbResponse = await _channelEnforcementRepository.CreateChannelEnforcementAsync(new ChannelEnforcement
			{
				GuildId = guildId,
				ChannelId = channelId
			});
			if (dbResponse != null)
			{
				return new ServiceResponse<ChannelEnforcement>(dbResponse);
			}
			return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Failed to create the channel enforcement");
		}

		public async Task<ServiceResponse<ChannelEnforcement>> ConfigureChannelEnforcementAsync(ulong guildId, ulong channelId, EEnforcementType type, string value)
		{
			var channelEnforcement = await GetChannelEnforcementAsync(guildId, channelId);
			if (!channelEnforcement.Success)
			{
				return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Channel enforcement doesn't exist");
			}

			var alteredChannelEnforcement = channelEnforcement.ServiceObject;

			// Update the enforcement object
			var trueValues = new string[] { "1", "true", "t", "yes", "y", ":thumbsup:" };
			var falseValues = new string[] { "0", "false", "f", "no", "n", ":thumbsdown:" };
			switch (type)
			{
				case EEnforcementType.Unknown:
					return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Unknown type to configure");
				case EEnforcementType.RestrictText:
					if (trueValues.Contains(value))
					{
						alteredChannelEnforcement.RestrictText = true;
					}
					else if (falseValues.Contains(value))
					{
						alteredChannelEnforcement.RestrictText = false;
					}
					else
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Unknown value");
					}
					break;
				case EEnforcementType.RestrictPictures:
					if (trueValues.Contains(value))
					{
						alteredChannelEnforcement.RestrictPictures = true;
					}
					else if (falseValues.Contains(value))
					{
						alteredChannelEnforcement.RestrictPictures = false;
					}
					else
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Unknown value");
					}
					break;
				case EEnforcementType.RequiresPictures:
					if (trueValues.Contains(value))
					{
						alteredChannelEnforcement.RequirePictures = true;
					}
					else if (falseValues.Contains(value))
					{
						alteredChannelEnforcement.RequirePictures = false;
					}
					else
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Unknown value");
					}
					break;
				case EEnforcementType.RequiresText:
					if (trueValues.Contains(value))
					{
						alteredChannelEnforcement.RequireText = true;
					}
					else if (falseValues.Contains(value))
					{
						alteredChannelEnforcement.RequireText = false;
					}
					else
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Unknown value");
					}
					break;
				case EEnforcementType.MinimumCharacters:
					try
					{
						alteredChannelEnforcement.MinimumCharacters = Convert.ToInt32(value);
					}
					catch (Exception)
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Invalid value");
					}
					break;
				case EEnforcementType.MinimumDiscordAgeDays:
					try
					{
						alteredChannelEnforcement.MinimumDiscordAgeDays = Convert.ToInt32(value);
					}
					catch (Exception)
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Invalid value");
					}
					break;
				case EEnforcementType.MinimumGuildJoinedAgeDays:
					try
					{
						alteredChannelEnforcement.MinimumGuildJoinedAgeDays = Convert.ToInt32(value);
					}
					catch (Exception)
					{
						return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Invalid value");
					}
					break;
			}

			// Update the object in the database
			var update = await _channelEnforcementRepository.UpdateChannelEnforcementAsync(alteredChannelEnforcement);
			if (update != null)
			{
				return new ServiceResponse<ChannelEnforcement>(update);
			}
			return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Failed to configure");
		}

		public async Task<ServiceResponse<ChannelEnforcement>> DeleteChannelEnforcementAsync(ulong guildId, ulong channelId)
		{
			var channelEnforcement = await GetChannelEnforcementAsync(guildId, channelId);
			if (!channelEnforcement.Success)
			{
				return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Channel enforcement doesn't exist");
			}

			// Update the object in the database
			var delete = await _channelEnforcementRepository.DeleteChannelEnforcementAsync(channelEnforcement.ServiceObject);
			if (delete != null)
			{
				return new ServiceResponse<ChannelEnforcement>(delete);
			}
			return new ServiceResponse<ChannelEnforcement>(null, success: false, message: "Failed to delete");
		}

		public async Task<ServiceResponse<AliasCommand>> GetAliasCommandByCommandAsync(ulong guildId, string command)
		{
			var aliasCommand = await _aliasCommandRepository.GetAliasCommandAsync(guildId, command);
			return new ServiceResponse<AliasCommand>(aliasCommand);
		}

		public async Task<ServiceResponse<AliasCommand>> CreateAliasCommandAsync(ulong guildId, string command, string aliasTo)
		{
			var existingEnforcement = await GetAliasCommandByCommandAsync(guildId, command);
			if (existingEnforcement?.ServiceObject != null)
			{
				return new ServiceResponse<AliasCommand>(null, success: false, message: "Alias command is already setup");
			}

			var dbResponse = await _aliasCommandRepository.CreateAliasCommandAsync(new AliasCommand
			{
				GuildId = guildId,
				Command = command,
				AliasTo = aliasTo
			});
			if (dbResponse != null)
			{
				return new ServiceResponse<AliasCommand>(dbResponse);
			}
			return new ServiceResponse<AliasCommand>(null, success: false, message: "Failed to create the alias command");
		}

		public async Task<ServiceResponse<AliasCommand>> EditAliasCommandAsync(ulong guildId, string command, string aliasTo)
		{
			var existingEnforcement = await GetAliasCommandByCommandAsync(guildId, command);
			if (existingEnforcement?.ServiceObject == null)
			{
				return new ServiceResponse<AliasCommand>(null, success: false, message: "Alias command is not setup");
			}

			existingEnforcement.ServiceObject.AliasTo = aliasTo;

			var dbResponse = await _aliasCommandRepository.UpdateAliasCommandAsync(existingEnforcement.ServiceObject);
			if (dbResponse != null)
			{
				return new ServiceResponse<AliasCommand>(dbResponse);
			}
			return new ServiceResponse<AliasCommand>(null, success: false, message: "Failed to update the alias command");
		}

		public async Task<ServiceResponse<AliasCommand>> DeleteAliasCommandAsync(ulong guildId, string command)
		{
			var existingEnforcement = await GetAliasCommandByCommandAsync(guildId, command);
			if (existingEnforcement?.ServiceObject == null)
			{
				return new ServiceResponse<AliasCommand>(null, success: false, message: "Alias command is not setup");
			}

			var dbResponse = await _aliasCommandRepository.DeleteAliasCommandAsync(existingEnforcement.ServiceObject);
			if (dbResponse != null)
			{
				return new ServiceResponse<AliasCommand>(dbResponse);
			}
			return new ServiceResponse<AliasCommand>(null, success: false, message: "Failed to delete the alias command");
		}
		#endregion
	}
}