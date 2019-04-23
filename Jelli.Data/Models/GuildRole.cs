using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jelli.Data.Models
{
	public class GuildRole
	{
		public ulong Id { get; set; }
		public ulong RoleId { get; set; }
		public string RoleDisplayName { get; set; }

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}