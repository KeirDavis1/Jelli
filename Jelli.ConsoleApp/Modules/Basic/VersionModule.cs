using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	public class VersionModule : ModuleBase<SocketCommandContext>
	{
		#region Methods
		[Command("version")]
		[Alias("v", "ver")]
		public Task PingAsync()
		{
			return ReplyAsync($"Jelli Bot - {Program.Version}");
		}
		#endregion
	}
}
