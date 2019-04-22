using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class EchoModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("echo")]
		public Task EchoAsync([Remainder] string text)
		{
			var embed = new EmbedBuilder
			{
				Title = "Echo",
				Description = text,
				Color = Context.Guild.GetHighestRole(Context.Guild.GetUser(Context.User.Id))?.Color
			};
			return ReplyAsync(embed: embed.Build());
		}
		#endregion
	}
}
