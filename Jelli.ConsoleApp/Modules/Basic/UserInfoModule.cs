using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class UserInfoModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("userinfo")]
		[RequireContext(ContextType.Guild)]
		public async Task UserInfoAsync(IGuildUser user = null)
		{
			try
			{
				user = user ?? (IGuildUser)Context.User;

				var description = $"{user.Status}";
				if (user.Activity != null)
				{
					description += $", {user.Activity.Type} {user.Activity.Name}";
				}

				var title = $"{user}";
				if (user.Nickname != null)
				{
					title += $"- {user.Nickname}";
				}

				var embed = new EmbedBuilder
				{
					Title = title,
					// TODO Implement a better way of describing the current user's activity
					Description = description,
					ThumbnailUrl = user.GetAvatarUrl(),
					Color = user.Guild.GetHighestRole(user)?.Color
				};
				// Joined Discord field
				embed.AddField("Joined Discord", $"{user.CreatedAt.ToString("F")} ({user.DaysSinceCreated()} days ago)", inline: true);
				// Joined server field
				embed.AddField("Joined server", $"{user.JoinedAt?.ToString("F")} ({user.DaysSinceJoined()} days ago)", inline: true);
				// Roles field
				var usersRoles = user.Guild.GetUserRolesList(user);
				if (usersRoles.Count > 0)
				{
					embed.AddField("Roles", $"{string.Join(", ", usersRoles)}")
						.WithFooter(footer => footer.Text = $"User ID {user.Id}");
				}

				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception)
			{
				await ReplyAsync("Failed to get user info");
			}
		}
		#endregion
	}
}
