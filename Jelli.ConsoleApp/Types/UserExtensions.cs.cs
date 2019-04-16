using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Discord.WebSocket;

namespace Jelli.ConsoleApp
{
	public static class UserExtensions
	{
		public static int DaysSinceCreated(this IUser user)
		{
			return (DateTime.UtcNow - user.CreatedAt).Days;
		}

		public static int? DaysSinceJoined(this IGuildUser user)
		{
			return (DateTime.UtcNow - user.JoinedAt)?.Days;
		}

		public static List<string> GetUserRoles(this IGuild guild, IGuildUser user)
		{
			var result = from serverRoles in guild.Roles.Where(role => guild.Id != role.Id) // Guild ID And @everyone ID is the same
									 from userRoles in user.RoleIds.Where(x => x == serverRoles.Id).DefaultIfEmpty()
									 select serverRoles;
			return result.Select(q => q.Name).ToList();
		}
	}
}
