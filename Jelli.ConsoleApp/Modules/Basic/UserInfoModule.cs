using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class UserInfoModule : ModuleBase<SocketCommandContext>
	{

		[Command("userinfo")]
		public Task UserInfoAsync(IGuildUser user = null)
		{
			user = user ?? (IGuildUser) Context.User;

			var embed = new EmbedBuilder
			{
				Title = $"{user} - {user.Nickname}",
				Description = $"{user.Status}, ${user.Activity.Name}",
				ThumbnailUrl = user.GetAvatarUrl(),
				Color = Color.Orange
			};
			// Joined Discord field
			embed.AddField("Joined Discord", $"{user.CreatedAt.ToString("F")} ({user.DaysSinceCreated()} days ago)", inline: true);
			// Joined server field
			embed.AddField("Joined server", $"{user.JoinedAt?.ToString("F")} ({user.DaysSinceJoined()} days ago)", inline: true);
			// Roles field
			embed.AddField("Roles", $"{string.Join(", ", user.Guild.GetUserRoles(user))}")
				.WithFooter(footer => footer.Text = $"User ID {user.Id}");

			return ReplyAsync(embed: embed.Build());
		}
	}
}
