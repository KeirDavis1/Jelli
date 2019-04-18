using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jelli.Data.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Services
{
	public class CommandHandlingService
	{
		#region Properties
		private readonly CommandService _commands;
		private readonly DiscordSocketClient _discord;
		private readonly IServiceProvider _services;
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public CommandHandlingService(IServiceProvider services, IGuildService guildService)
		{
			_commands = services.GetRequiredService<CommandService>();
			_discord = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			_guildService = guildService;

			// Hook CommandExecuted to handle post-command-execution logic.
			_commands.CommandExecuted += CommandExecutedAsync;
			// Hook MessageReceived so we can process each message to see
			// if it qualifies as a command.
			_discord.MessageReceived += MessageReceivedAsync;
		}
		#endregion

		#region Methods
		public async Task InitializeAsync()
		{
			// Register modules that are public and inherit ModuleBase<T>.
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
		}

		public async Task MessageReceivedAsync(SocketMessage rawMessage)
		{
			// Ignore system messages, or messages from other bots
			if (!(rawMessage is SocketUserMessage message)) return;
			if (message.Source != MessageSource.User) return;

			// This value holds the offset where the prefix ends
			var argPos = 0;

			var commandPrefix = "!";

			// Get guild from the message
			var socketGuildChannel = message.Channel as SocketGuildChannel;
			var messageGuild = socketGuildChannel?.Guild;

			// Are we in a guild?
			if (messageGuild != null)
			{
				// Get the guild from the database
				var guild = await _guildService.GetGuildCommandPrefixAsync(messageGuild.Id);
				if (guild.Success)
				{
					// Get the command prefix
					commandPrefix = guild.ServiceObject ?? commandPrefix;
				}
			}

			if (
				// User can tag the bot to use it.
				!message.HasMentionPrefix(_discord.CurrentUser, ref argPos) &&
				// User can use a prefix to use it.
				!message.HasStringPrefix(commandPrefix, ref argPos)
			)
			{
				return;
			}

			var context = new SocketCommandContext(_discord, message);
			// Perform the execution of the command. In this method,
			// the command service will perform precondition and parsing check
			// then execute the command if one is matched.
			await _commands.ExecuteAsync(context, argPos, _services);
			// Note that normally a result will be returned by this format, but here
			// we will handle the result in CommandExecutedAsync,
		}

		public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
		{
			// command is unspecified when there was a search failure (command not found); we don't care about these errors
			if (!command.IsSpecified)
				return;

			// the command was successful, we don't care about this result, unless we want to log that a command succeeded.
			if (result.IsSuccess)
				return;

			// the command failed, let's notify the user that something happened.
			await context.Channel.SendMessageAsync($"error: {result}");
		}
		#endregion
	}
}
