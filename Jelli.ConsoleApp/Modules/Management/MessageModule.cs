using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jelli.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Miscellaneous
{
	[Group("m")]
	[Alias("msg")]
	[RequireContext(ContextType.Guild)]
	public class MessageModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("post")]
		[Alias("+", "p")]
		[RequireUserPermission(GuildPermission.ManageChannels)]
		public async Task CustomMessagePostAsync(ISocketMessageChannel channel, [Remainder] string message)
		{
			try
			{
				// Send the message to the given channel
				await channel.SendMessageAsync(message);
			}
			catch (Exception)
			{
				await ReplyAsync("Failed to post custom message");
			}
		}
		#endregion
	}
}
