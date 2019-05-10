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
using System.Threading;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Services
{
	public class DiscordDisconnectService
	{
		#region Properties
		private readonly DiscordSocketClient _discord;
		private readonly IServiceProvider _services;
		private readonly BotPersistence _botPersistence;
		#endregion

		#region Constructor
		public DiscordDisconnectService(IServiceProvider services, BotPersistence botPersistence)
		{
			_discord = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			_botPersistence = botPersistence;

			_discord.Disconnected += DisconnectHandler;
		}
		#endregion

		#region Methods
		public Task DisconnectHandler(Exception exception)
		{
			try
			{
				_botPersistence.DiscordDisconnects++;
			}
			catch (Exception)
			{
				// Failed to get guild from the database
				System.Console.WriteLine("Failed to add a disconnect. Possibly concurrency issue.");
			}
			return Task.CompletedTask;
		}
		#endregion
	}
}
