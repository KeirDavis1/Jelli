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
	public class IssueModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("issue")]
		public async Task IssueCommandAsync()
		{
			try
			{
				// Try and send the message to the user
				await Context.User.SendMessageAsync("Hey there! Thanks for spotting an issue!\nYou can submit issues here: https://github.com/keirdavis1/jelli/issues");
			}
			catch (Exception)
			{
				await ReplyAsync("You can submit issues here: https://github.com/keirdavis1/jelli/issues");
			}
		}
		#endregion
	}
}
