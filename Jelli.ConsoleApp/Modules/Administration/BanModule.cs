using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Administration
{
	public class BanModule : ModuleBase<SocketCommandContext>
	{
		[Command("ban")]
		[RequireContext(ContextType.Guild)]
		[RequireUserPermission(GuildPermission.BanMembers)]
		[RequireBotPermission(GuildPermission.BanMembers)]
		public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
		{
			await user.Guild.AddBanAsync(user, reason: reason);
			await ReplyAsync("ok!");
		}
	}
}
