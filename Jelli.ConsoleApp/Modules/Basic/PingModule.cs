using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class PingModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("ping")]
		[Alias("pong", "hello")]
		public Task PingAsync()
		{
			return ReplyAsync("Pong!");
		}
		#endregion
	}
}
