using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Jelli.ConsoleApp.Services
{
	public class StaticReplyService
	{
		#region Properties
		private readonly DiscordSocketClient _discord;
		private readonly IServiceProvider _services;
		#endregion

		#region Constructor
		public StaticReplyService(IServiceProvider services)
		{
			_discord = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			_discord.MessageReceived += MessageReceivedAsync;
		}
		#endregion

		#region Methods
		public async Task MessageReceivedAsync(SocketMessage rawMessage)
		{
			// Ignore messages from ourself
			if (!(rawMessage is SocketUserMessage message)) return;
			if (message.Author.Id == _discord.CurrentUser.Id) return;

			if (message.Content.Equals("ayy", StringComparison.CurrentCultureIgnoreCase))
			{
				await message.Channel.SendMessageAsync("lmao");
			}
			else
			{
				var regex = new Regex(@"^\:D+$");
				if (regex.IsMatch(message.Content))
					await message.Channel.SendMessageAsync(message.Content + "D");
			}
		}
		#endregion
	}
}