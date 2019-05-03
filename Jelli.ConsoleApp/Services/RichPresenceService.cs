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
	public class RichPresenceService
	{
		#region Properties
		private readonly DiscordSocketClient _discord;
		private readonly IServiceProvider _services;
		private readonly IGuildService _guildService;
		private readonly Timer _timer;
		#endregion

		#region Constructor
		public RichPresenceService(IServiceProvider services, IGuildService guildService)
		{
			_discord = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			_guildService = guildService;
			_timer = new Timer(_ => SetPresence(), null, TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(5));
		}
		#endregion

		#region Methods
		public async void SetPresence()
		{
			try
			{
				var guilds = _discord.Guilds;
				var userCount = guilds.Sum(a => a.Users.Count());
				var guildCount = guilds.Count();

				await _discord.SetGameAsync($"{guildCount} servers | {userCount} users", "", ActivityType.Watching);
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
