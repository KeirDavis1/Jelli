using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class EchoModule : ModuleBase<SocketCommandContext>
	{
		[Command("echo")]
		public Task EchoAsync([Remainder] string text)
		{
			return ReplyAsync('\u200B' + text);
		}
	}
}
