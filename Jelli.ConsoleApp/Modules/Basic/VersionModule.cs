using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class VersionModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("version")]
		[Alias("v", "ver")]
		public Task VersionAsync()
		{
			var embed = new EmbedBuilder
			{
				Title = "Version",
				Description = $":tada: We're running on version {Program.Version}!",
				ThumbnailUrl = Context.Guild.CurrentUser.GetAvatarUrl(),
				Color = Context.Guild.GetHighestRole(Context.Guild.CurrentUser)?.Color
			};

			return ReplyAsync(embed: embed.Build());
		}
		#endregion
	}
}
