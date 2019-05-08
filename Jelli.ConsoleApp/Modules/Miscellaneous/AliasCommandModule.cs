using Discord;
using Discord.Commands;
using Jelli.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Miscellaneous
{
	[Group("aliascommand")]
	[Alias("ac")]
	[RequireContext(ContextType.Guild)]
	public class AliasCommandModule : ModuleBase<SocketCommandContext>
	{
		#region Properties
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public AliasCommandModule(IGuildService guildService)
		{
			_guildService = guildService;
		}
		#endregion

		#region Methods
		[Command("create")]
		[Alias("new", "add")]
		[RequireUserPermission(GuildPermission.ManageChannels)]
		public async Task AliasCommandCreateAsync(string command, [Remainder] string aliasTo)
		{
			try
			{
				// Create the custom command with the response
				var serviceResponse = await _guildService.CreateAliasCommandAsync(Context.Guild.Id, command, aliasTo);

				// Did the response succeed?
				if (!serviceResponse.Success)
				{
					// Created failed, let the user know.
					await ReplyAsync("Failed to create alias command at this time");
					return;
				}
				// Let the user know the command was created
				await ReplyAsync("Alias command created");
			}
			catch (Exception)
			{
				await ReplyAsync("Failed to execute alias command");
			}
		}
		#endregion
	}
}
