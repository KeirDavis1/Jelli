using System.ComponentModel.DataAnnotations.Schema;

namespace Jelli.Data.Models
{
	public class Guild
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public ulong GuildId { get; set; }
		public string CommandPrefix { get; set; }
	}
}