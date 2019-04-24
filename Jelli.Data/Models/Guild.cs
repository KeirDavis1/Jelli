using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jelli.Data.Models
{
	public class Guild
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public ulong GuildId { get; set; }
		public string CommandPrefix { get; set; }

		public List<GuildRole> GuildRoles { get; set; }
		public List<GuildUserNote> GuildUserNotes { get; set; }
	}
}