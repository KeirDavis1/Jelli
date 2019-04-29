using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jelli.Data.Models
{
	public class ChannelEnforcement
	{
		public ulong Id { get; set; }
		public ulong ChannelId { get; set; }
		public bool RestrictPictures { get; set; }
		public bool RestrictText { get; set; }
		public int MinimumGuildJoinedAgeDays { get; set; }
		public int MinimumDiscordAgeDays { get; set; }
		public int MinimumCharacters { get; set; }

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}