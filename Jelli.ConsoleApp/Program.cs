using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Jelli.ConsoleApp.Services;
using Jelli.Data;
using System;
using System.Threading.Tasks;
using Jelli.Data.Repositories.Interfaces;
using Jelli.Data.Services.Interfaces;
using Jelli.Data.Repositories;
using Jelli.Data.Services;
using Jelli.ConsoleApp.Types;
using Discord.Addons.Interactive;

namespace Jelli.ConsoleApp
{
	public class Program
	{
		#region Properties
		public static string Version = "v0.0.1";
		private CommandService _commands;
		#endregion

		#region Constructor
		public static void Main(string[] args)
		{
			/// Uncomment the following to test if you've added the token correctly
			//Console.WriteLine(Environment.GetEnvironmentVariable("token"));
			//Console.ReadKey();
			new Program().MainAsync().GetAwaiter().GetResult();
		}

		public Program(CommandService commands = null)
		{
			_commands = commands ?? new CommandService(new CommandServiceConfig
			{
				DefaultRunMode = RunMode.Async,
				CaseSensitiveCommands = false,
				LogLevel = LogSeverity.Verbose
			});
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
				services.GetRequiredService<ChannelEnforcerService>();
				services.GetRequiredService<RichPresenceService>();
				services.GetRequiredService<BotPersistence>();
				services.GetRequiredService<StaticReplyService>();

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
					.AddSingleton<BotPersistence>()
					.AddSingleton<DiscordSocketClient>()
					.AddSingleton(_commands)
					.AddSingleton<CommandHandlingService>()
					.AddSingleton<ChannelEnforcerService>()
					.AddSingleton<StaticReplyService>()
					.AddSingleton<RichPresenceService>()
					.AddSingleton<InteractiveService>()
					.AddMemoryCache()
					.AddEntityFrameworkSqlite()
					.AddDbContext<BotContext>()
					// Database repository access types
					.AddScoped<IGuildRepository, GuildRepository>()
					.AddScoped<IGuildRoleRepository, GuildRoleRepository>()
					.AddScoped<IGuildUserNoteRepository, GuildUserNoteRepository>()
					.AddScoped<IGuildCustomCommandRepository, GuildCustomCommandRepository>()
					.AddScoped<IChannelEnforcementRepository, ChannelEnforcementRepository>()
					// Service to interact with repositories
					.AddScoped<IGuildService, GuildService>()
					.BuildServiceProvider();
		}
		#endregion
	}


}
