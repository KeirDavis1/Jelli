using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class EchoModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("echo")]
		public Task EchoAsync([Remainder] string text)
		{
			// Reply with a zero width space prefixed
			// Stops us calling other bots, i.e. someone running !echo !ban @username
			return ReplyAsync('\u200B' + text);
		}
		#endregion
	}
}
