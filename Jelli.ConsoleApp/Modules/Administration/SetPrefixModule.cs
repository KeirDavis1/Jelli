using Discord;
using Discord.Commands;
using Jelli.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Administration
{
	public class SetPrefixModule : ModuleBase<SocketCommandContext>
	{
		#region Properties
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public SetPrefixModule(IGuildService guildService)
		{
			_guildService = guildService;
		}
		#endregion

		#region Methods
		[Command("setprefix")]
		[RequireContext(ContextType.Guild)]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task SetPrefixAsync([Remainder] string prefix)
		{
			var prefixCall = await _guildService.SetGuildCommandPrefixAsync(Context.Guild.Id, prefix);
			if (prefixCall.Success)
			{
				await ReplyAsync($"I will now listen to commands with a prefix of `{Format.Sanitize(prefix)}`");
			} else
			{
				await ReplyAsync($"I couldn't set the prefix, try again later.");
			}
		}
		#endregion

	}
}
