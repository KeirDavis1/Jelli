using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Discord.WebSocket;

namespace Jelli.ConsoleApp.Types
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

		public static List<IRole> GetUserRoles(this IGuild guild, IGuildUser user)
		{
			var result = from serverRoles in guild.Roles.Where(role => guild.Id != role.Id) // Guild ID And @everyone ID is the same
						 join userRoles in user.RoleIds on serverRoles.Id equals userRoles
						 select serverRoles;
			return result.ToList();
		}

		public static List<string> GetUserRolesList(this IGuild guild, IGuildUser user)
		{
			return GetUserRoles(guild, user).Select(q => q.Name).ToList();
		}

		public static IRole GetHighestRole(this IGuild guild, IGuildUser user)
		{
			var roles = GetUserRoles(guild, user);

			return roles.OrderByDescending(role => role.Position).FirstOrDefault();
		}
	}
}
