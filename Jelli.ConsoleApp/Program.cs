﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jelli.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp
{
	public class Program
	{
		#region Properties
		private readonly DiscordSocketClient _client;
		#endregion

		#region Constructor
		public static void Main(string[] args)
		{
			//Console.WriteLine(Environment.GetEnvironmentVariable("token"));
			//Console.ReadKey();
			new Program().MainAsync().GetAwaiter().GetResult();
		}
		#endregion

		#region Methods
		public async Task MainAsync()
		{
			using (var services = ConfigureServices())
			{
				var client = services.GetRequiredService<DiscordSocketClient>();

				client.Log += LogAsync;
				services.GetRequiredService<CommandService>().Log += LogAsync;

				await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("token"));
				await client.StartAsync();

				await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

				await Task.Delay(-1);
			}
		}

		private Task LogAsync(LogMessage log)
		{
			Console.WriteLine(log.ToString());

			return Task.CompletedTask;
		}

		private ServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
					.AddSingleton<DiscordSocketClient>()
					.AddSingleton<CommandService>()
					.AddSingleton<CommandHandlingService>()
					.BuildServiceProvider();
		}
		#endregion
	}


}
