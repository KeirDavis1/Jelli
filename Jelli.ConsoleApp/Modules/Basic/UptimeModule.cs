using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using System.Linq;
using System;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class UptimeModule : ModuleBase<SocketCommandContext>
	{
		#region Properties
		private readonly BotPersistence _botPersistence;
		#endregion

		#region Constructor
		public UptimeModule(BotPersistence botPersistence)
		{
			_botPersistence = botPersistence;
		}
		#endregion

		#region Methods
		[Command("uptime")]
		[Alias("ut")]
		public async Task UptimeAsync()
		{
			try
			{
				var embedColour = Color.Blue;
				if (Context.Guild != null)
				{
					embedColour = Context.Guild.GetHighestRole(Context.Guild.CurrentUser)?.Color ?? embedColour;
				}

				var replyDescription = $":timer: We've been running for {_botPersistence.GetUptime().ToString()}";

				var embed = new EmbedBuilder
				{
					Title = "Uptime",
					Description = replyDescription,
					ThumbnailUrl = Context.Client.CurrentUser.GetAvatarUrl(),
					Color = embedColour
				};

				try
				{
					await ReplyAsync(embed: embed.Build());
				}
				catch (Exception)
				{
					// This may occurr when there is no permission for embeds
					await ReplyAsync(replyDescription);
				}
			}
			catch (Exception)
			{
				await ReplyAsync("Failed to get uptime");
			}
		}
		#endregion
	}
}