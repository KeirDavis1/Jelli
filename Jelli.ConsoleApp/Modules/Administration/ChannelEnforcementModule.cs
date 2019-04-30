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
using Jelli.Data.Types;


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
				var response = await _guildService.CreateChannelEnforcementAsync(Context.Guild.Id, channel.Id);
				if (response.Success)
				{
					await ReplyAsync("A channel has been setup to be enforced");
					return;
				}
				Console.WriteLine(response.Message);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
			}
			await ReplyAsync("Channel failed to be enforced");
		}

		[RequireUserPermission(GuildPermission.ManageChannels)]
		[Command("configure")]
		[Alias("c", "conf", "edit", "alter")]
		public async Task ChannelEnforcedChannelConfigureAsync(IGuildChannel channel, string attribute, string value)
		{
			try
			{
				var response = await _guildService.ConfigureChannelEnforcementAsync(Context.Guild.Id, channel.Id, attribute.FromStringToEnforcementType(), value);
				if (response.Success)
				{
					await ReplyAsync("The channel has been successfully configured");
					return;
				}
				Console.WriteLine(response.Message);
			}
			catch (Exception)
			{

			}
			await ReplyAsync("Channel failed to be configured");
		}

		[RequireUserPermission(GuildPermission.ManageChannels)]
		[Command("delete")]
		[Alias("d", "-", "remove", "stop", "disable")]
		public async Task ChannelEnforcedChannelDeleteAsync(IGuildChannel channel)
		{
			try
			{
				var response = await _guildService.DeleteChannelEnforcementAsync(Context.Guild.Id, channel.Id);
				if (response.Success)
				{
					await ReplyAsync("The configured channel has now been deleted");
					return;
				}
				Console.WriteLine(response.Message);
			}
			catch (Exception)
			{

			}
			await ReplyAsync("Configured channel failed to be deleted");
		}
		#endregion
	}
}
