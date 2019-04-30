using System.Threading.Tasks;
using Jelli.Data.Models;
using Jelli.Data.Repositories.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Jelli.Data.Repositories
{
	public class ChannelEnforcementRepository : IChannelEnforcementRepository
	{
		#region Properties
		private readonly BotContext _context;
		#endregion

		#region Constructor
		public ChannelEnforcementRepository(BotContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public async Task<ChannelEnforcement> CreateChannelEnforcementAsync(ChannelEnforcement channelEnforcement)
		{
			await _context.AddAsync(channelEnforcement);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return channelEnforcement;
			}
			return null;

		}

		public async Task<ChannelEnforcement> DeleteChannelEnforcementAsync(ChannelEnforcement channelEnforcement)
		{
			_context.Remove(channelEnforcement);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return channelEnforcement;
			}
			return null;
		}

		public async Task<ChannelEnforcement> GetChannelEnforcementAsync(ulong guildId, ulong channelId)
		{
			return await _context.ChannelEnforcements.FirstOrDefaultAsync(g => g.GuildId == guildId && g.ChannelId == channelId);
		}

		public async Task<ChannelEnforcement> UpdateChannelEnforcementAsync(ChannelEnforcement channelEnforcement)
		{
			_context.Update(channelEnforcement);
			var savedChanges = await _context.SaveChangesAsync();

			if (savedChanges > 0)
			{
				return channelEnforcement;
			}
			return null;
		}
		#endregion
	}
}