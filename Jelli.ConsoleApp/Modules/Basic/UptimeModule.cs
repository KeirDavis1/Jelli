using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using System.Linq;

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
		public Task UptimeAsync()
		{
			var embed = new EmbedBuilder
			{
				Title = "Uptime",
				Description = $":timer: We've been running for {_botPersistence.GetUptime().ToString()}",
				ThumbnailUrl = Context.Guild.CurrentUser.GetAvatarUrl(),
				Color = Context.Guild.GetHighestRole(Context.Guild.CurrentUser)?.Color
			};

			return ReplyAsync(embed: embed.Build());
		}
		#endregion
	}
}