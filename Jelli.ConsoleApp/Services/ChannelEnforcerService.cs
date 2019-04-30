using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jelli.ConsoleApp.Types;
using Jelli.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Services
{
	public class ChannelEnforcerService
	{
		#region Properties
		private readonly DiscordSocketClient _discord;
		private readonly IServiceProvider _services;
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public ChannelEnforcerService(IServiceProvider services, IGuildService guildService)
		{
			_discord = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			_guildService = guildService;

			_discord.MessageReceived += MessageReceivedAsync;
		}
		#endregion

		#region Methods
		public async Task MessageReceivedAsync(SocketMessage rawMessage)
		{
			// Ignore messages from ourself
			if (!(rawMessage is SocketUserMessage message)) return;
			if (message.Author.Id == _discord.CurrentUser.Id) return;

			// Get guild from the message
			var socketGuildChannel = message.Channel as SocketGuildChannel;
			var messageGuild = socketGuildChannel?.Guild;

			// Are we in a guild?
			if (messageGuild == null)
			{
				return;
			}
			try
			{
				// Get the guild from the database
				var enforcement = await _guildService.GetChannelEnforcementAsync(messageGuild.Id, message.Channel.Id);
				if (!enforcement.Success)
				{
					return;
				}
				// We're enforcing this channel
				var enforcer = enforcement.ServiceObject;
				if (
					// Minimum character enforcement
					(enforcer.MinimumCharacters != null && enforcer.MinimumCharacters > message.Content.Length) ||
					// No text enforcement
					(enforcer.RestrictText != null && message.Content.Length > 0) ||
					// No picture enforcement
					(enforcer.RestrictPictures != null && message.Embeds.Any()) ||
					// Enforce minimum discord age
					(enforcer.MinimumDiscordAgeDays != null && enforcer.MinimumDiscordAgeDays > ((IUser)message.Author).DaysSinceCreated()) ||
					// Enforce minimum guild age
					(enforcer.MinimumGuildJoinedAgeDays != null && enforcer.MinimumGuildJoinedAgeDays > ((IGuildUser)message.Author).DaysSinceJoined())
				)
				{
					await message.DeleteAsync();
				}
			}
			catch (Exception)
			{
				// Failed to get guild from the database
				System.Console.WriteLine("Failed to find enforce channel because of an exception");
			}
		}
		#endregion
	}
}
