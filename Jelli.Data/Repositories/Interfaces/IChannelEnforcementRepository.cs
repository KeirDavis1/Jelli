using System.Threading.Tasks;
using Jelli.Data.Models;

namespace Jelli.Data.Repositories.Interfaces
{
	public interface IChannelEnforcementRepository
	{
		Task<ChannelEnforcement> GetChannelEnforcementAsync(ulong guildId, ulong channelId);

		Task<ChannelEnforcement> CreateChannelEnforcementAsync(ChannelEnforcement channelEnforcement);

		Task<ChannelEnforcement> DeleteChannelEnforcementAsync(ChannelEnforcement channelEnforcement);

		Task<ChannelEnforcement> UpdateChannelEnforcementAsync(ChannelEnforcement channelEnforcement);
	}
}