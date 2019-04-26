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
		public async Task VersionAsync()
		{
			var embedColour = Color.Blue;
			if (Context.Guild != null)
			{
				embedColour = Context.Guild.GetHighestRole(Context.Guild.CurrentUser)?.Color ?? embedColour;
			}

			var replyDescription = $":tada: We're running on version {Program.Version}!";

			var embed = new EmbedBuilder
			{
				Title = "Version",
				Description = replyDescription,
				ThumbnailUrl = Context.Client.CurrentUser.GetAvatarUrl(),
				Color = embedColour
			};

			try
			{
				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception)
			{
				// This may occurr when there is no permission for embeds
				await ReplyAsync(replyDescription);
			}
		}
		#endregion
	}
}
