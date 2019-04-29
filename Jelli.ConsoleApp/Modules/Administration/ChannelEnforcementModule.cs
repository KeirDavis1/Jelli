using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using Jelli.Data.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Addons.Interactive;


namespace Jelli.ConsoleApp.Modules.Administration
{
	[Group("channelenforcer")]
	[Alias("ce", "channelenforcement", "enforcer", "enforce", "cenforce")]
	[RequireContext(ContextType.Guild)]
	public class ChannelEnforcementModule : InteractiveBase
	{
		#region Properties
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public ChannelEnforcementModule(IGuildService guildService)
		{
			_guildService = guildService;
		}
		#endregion

		#region Methods
		[RequireUserPermission(GuildPermission.ManageChannels)]
		[Command("add")]
		[Alias("create", "new", "n", "+")]
		public async Task ChannelEnforcedChannelAddAsync(IGuildChannel channel)
		{
			try
			{
				await ReplyAsync("In progress");
			}
			catch (Exception)
			{
				await ReplyAsync("Channel failed to be enforced");
			}

		}

		[RequireUserPermission(GuildPermission.ManageChannels)]
		[Command("configure")]
		[Alias("c", "conf", "edit", "alter")]
		public async Task ChannelEnforcedChannelConfigureAsync(IGuildChannel channel, string attribute, string value)
		{
			try
			{
				await ReplyAsync($"In progress");
			}
			catch (Exception)
			{
				await ReplyAsync("Channel failed to be configured");
			}
		}

		[RequireUserPermission(GuildPermission.ManageChannels)]
		[Command("configure")]
		[Alias("c", "conf", "edit", "alter")]
		public async Task ChannelEnforcedChannelDeleteAsync(IGuildChannel channel)
		{
			try
			{
				await ReplyAsync("In progress");
			}
			catch (Exception)
			{
				await ReplyAsync("Channel failed to be configured");
			}
		}
		#endregion
	}
}
