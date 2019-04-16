using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules
{
	public class PublicModule : ModuleBase<SocketCommandContext>
	{
		[Command("ping")]
		[Alias("pong", "hello")]
		public Task PingAsync()
		{
			return ReplyAsync("Pong!");
		}

		[Command("userinfo")]
		public async Task UserInfoAsync(IUser user = null)
		{
			user = user ?? Context.User;

			await ReplyAsync(user.ToString());
		}

		[Command("ban")]
		[RequireContext(ContextType.Guild)]
		[RequireUserPermission(GuildPermission.BanMembers)]
		[RequireBotPermission(GuildPermission.BanMembers)]
		public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
		{
			await user.Guild.AddBanAsync(user, reason: reason);
			await ReplyAsync("ok!");
		}

		[Command("echo")]
		public Task EchoAsync([Remainder] string text)
		{
			return ReplyAsync('\u200B' + text);
		}

		[Command("list")]
		public Task ListAsync(params string[] objects)
		{
			return ReplyAsync("You listed: " + string.Join("; ", objects));
		}

		[Command("guild_only")]
		[RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
		public Task GuildOnlyCommand()
		{
			return ReplyAsync("Nothing to see here!");
		}
	}
}
