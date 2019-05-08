using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jelli.Data.Models
{
	public class AliasCommand
	{
		public ulong Id { get; set; }
		public string Command { get; set; }
		public string AliasTo { get; set; }

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}