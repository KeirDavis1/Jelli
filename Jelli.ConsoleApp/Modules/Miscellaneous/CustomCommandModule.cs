using Discord.Commands;
using Jelli.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Miscellaneous
{
	[Group("customcommand")]
	[Alias("cc")]
	[RequireContext(ContextType.Guild)]
	public class CustomCommandModule : ModuleBase<SocketCommandContext>
	{
		#region Properties
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public CustomCommandModule(IGuildService guildService)
		{
			_guildService = guildService;
		}
		#endregion

		#region Methods
		[Command("create")]
		[Alias("new", "add")]
		public async Task CustomCommandCreateAsync(string command, [Remainder] string response)
		{
			// Create the custom command with the response
			var serviceResponse = await _guildService.CreateGuildCustomCommandAsync(Context.Guild.Id, command, response);

			// Did the response succeed?
			if (!serviceResponse.Success)
			{
				// Created failed, let the user know.
				await ReplyAsync("Failed to create custom command at this time");
				return;
			}
			// Let the user know the command was created
			await ReplyAsync("Custom command created");
		}
		#endregion
	}
}
