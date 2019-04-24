using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using Jelli.Data.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Addons.Interactive;


namespace Jelli.ConsoleApp.Modules.Administration
{
	[Group("notes")]
	[RequireContext(ContextType.Guild)]
	public class NoteModule : InteractiveBase
	{
		#region Properties
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public NoteModule(IGuildService guildService)
		{
			_guildService = guildService;
		}
		#endregion

		#region Methods
		[RequireUserPermission(GuildPermission.ManageChannels)]
		[RequireBotPermission(GuildPermission.SendMessages)]
		[Command("add")]
		[Alias("create", "new")]
		public async Task NotesAddAsync(IGuildUser user, [Remainder] string note)
		{
			var response = await _guildService.CreateGuildUserNoteAsync(Context.Guild.Id, user.Id, Context.User.Id, note);
			if (response.Success)
			{
				await ReplyAsync("Note added.");
				return;
			}
			await ReplyAsync("Note failed to add.");
		}

		[RequireUserPermission(GuildPermission.ManageChannels)]
		[RequireBotPermission(GuildPermission.SendMessages)]
		[Command("list")]
		[Alias("get", "show")]
		public async Task NotesListAsync(IGuildUser user)
		{
			var response = await _guildService.GetGuildUserNotesAsync(Context.Guild.Id, user.Id);
			if (response.Success)
			{
				var pages = new List<string>();
				int page = 0;
				foreach (var note in response.ServiceObject)
				{
					if (pages.ElementAtOrDefault(page) == null)
					{
						pages.Add("");
					}
					var pageContent = pages[page];

					var content = note.Content;
					if (note.Content.Length > 1024)
					{
						content = note.Content.Substring(0, 1024);
					}

					var submitterName = "";
					var submitterUser = Context.Guild.Users.FirstOrDefault(a => a.Id == note.SubmitterId);
					if (submitterUser == null)
					{
						submitterName = note.SubmitterId.ToString();
					}
					else
					{
						submitterName = submitterUser.Username;
					}

					pageContent += $"**[ID {note.Id}] [{note.Created.ToString("o")}] Submitted by {submitterName}**\n{content}\n";
					pages[page] = pageContent;

					if (pageContent.Length > 1024)
					{
						page++;
					}
				}

				if (!pages.Any())
				{
					await ReplyAsync("There's no notes for that user.");
				}
				else
				{
					await PagedReplyAsync(pages.ToList());
				}
				return;
			}
			await ReplyAsync("Note failed to get.");
		}
		#endregion
	}
}
