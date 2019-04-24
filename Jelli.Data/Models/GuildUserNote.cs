using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jelli.Data.Models
{
	public class GuildUserNote
	{
		public ulong Id { get; set; }
		public ulong UserId { get; set; }
		public ulong SubmitterId { get; set; }
		public string Content { get; set; }

		public DateTime Created { get; set; }

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}