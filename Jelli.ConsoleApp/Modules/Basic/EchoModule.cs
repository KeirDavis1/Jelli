using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
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
		[RequireBotPermission(ChannelPermission.EmbedLinks)]
		public async Task EchoAsync([Remainder] string text)
		{
			try
			{
				var embedColour = Color.Blue;
				if (Context.Guild != null)
				{
					embedColour = Context.Guild.GetHighestRole(Context.Guild.CurrentUser)?.Color ?? embedColour;
				}

				var embed = new EmbedBuilder
				{
					Title = "Echo",
					Description = text,
					Color = embedColour
				};
				try
				{
					await ReplyAsync(embed: embed.Build());
				}
				catch (Exception)
				{
					await ReplyAsync("I don't have permissions to echo");
				}
			}
			catch (Exception)
			{
				await ReplyAsync("Failed to echo message");
			}
		}
		#endregion
	}
}
