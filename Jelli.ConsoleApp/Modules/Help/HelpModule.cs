using System.Text;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using Discord.Commands;
using System.Linq;
using Discord;
using System.Collections.Generic;

namespace Jelli.ConsoleApp.Modules.Help
{
	public class HelpModule : InteractiveBase
	{
		#region Properties
		private readonly CommandService _commandService;
		#endregion

		#region Constructor
		public HelpModule(CommandService commandService)
		{
			_commandService = commandService;
		}
		#endregion

		#region Methods

		#endregion
		[Command("help")]
		public async Task HelpAsync()
		{
			var helpCommands = new List<string>();

			foreach (var module in _commandService.Modules)
			{
				// Ignore this module
				if (module.Name == "HelpModule")
					continue;

				// Iterate through the commands of a module
				foreach (var cmd in module.Commands)
				{
					// Build a small description
					var description = new StringBuilder();
					description.Append($"{cmd.Aliases.First()} ");
					if (cmd.Parameters.Any())
						description.Append($"[{string.Join(", ", cmd.Parameters.Select(p => p.Name))}]");
					description.Append("\n");

					// Append it to the list of commands
					helpCommands.Add(description.ToString());
				}
			}

			var helpPages = new List<string>();
			var page = 0;

			foreach (var command in helpCommands)
			{
				var pageContent = helpPages.ElementAtOrDefault(page);
				if (pageContent == null)
				{
					// Page doesn't exist so create the page
					pageContent = command;
					helpPages.Add(pageContent);
					// Go to the next command
					continue;
				}

				// Apend the content
				pageContent += command;
				helpPages[page] = pageContent;

				// Max page length is set to 1024 (up to 2048)
				if (pageContent.Length >= 1024 - command.Length)
				{
					page++;
				}
			}

			var pages = helpPages.ToList();
			await PagedReplyAsync(pages);
		}
	}
}