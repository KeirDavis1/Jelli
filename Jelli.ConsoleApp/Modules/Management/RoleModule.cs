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

namespace Jelli.ConsoleApp.Modules.Management
{
	[Group("role")]
	[RequireContext(ContextType.Guild)]
	public class RoleModule : InteractiveBase
	{
		#region Properties
		private readonly IGuildService _guildService;
		#endregion

		#region Constructor
		public RoleModule(IGuildService guildService)
		{
			_guildService = guildService;
		}
		#endregion

		#region Methods
		[RequireBotPermission(GuildPermission.ManageRoles)]
		[Command("apply")]
		public async Task RoleApplyAsync([Remainder] string displayName)
		{
			var response = await _guildService.GetGuildRoleAsync(Context.Guild.Id, displayName);
			if (response.Success)
			{
				var guildUser = (IGuildUser)Context.User;
				var guildRole = Context.Guild.Roles.FirstOrDefault(a => a.Id == response.ServiceObject.RoleId);

				// Does the role exist in the server?
				if (guildRole != null)
				{
					// Add role to user
					await guildUser.AddRoleAsync((IRole)guildRole);
					await ReplyAsync("Role applied!");
					return;
				}
				else
				{
					// TODO Delete the role
				}
			}
			await ReplyAsync("Couldn't find that role");
		}

		[Command("list")]
		[Alias("show", "all")]
		public async Task RoleListAsync()
		{
			var response = await _guildService.GetGuildRolesAsync(Context.Guild.Id);
			if (!response.Success)
			{
				// Database couldn't get the guild roles
				await ReplyAsync("Could not get roles at this time.");
				return;
			}
			if (!response.ServiceObject.Any())
			{
				// No roles found in the database
				await ReplyAsync("There are no roles available at this time.");
				return;
			}
			var message = string.Join("\n", response.ServiceObject.Select(a => a.RoleDisplayName));

			var paginatedMessage = new PaginatedMessage
			{
				Title = $"Available Roles for {Context.Guild.Name}",
				Pages = message.TextToPages(1536)
			};

			await PagedReplyAsync(paginatedMessage);
		}


		[RequireBotPermission(GuildPermission.ManageRoles)]
		[Command("relieve")]
		[Alias("revoke", "remove", "leave")]
		public async Task RoleRelieveAsync(string displayName)
		{
			var response = await _guildService.GetGuildRoleAsync(Context.Guild.Id, displayName);
			if (response.Success)
			{
				var guildUser = (IGuildUser)Context.User;
				var guildRole = Context.Guild.Roles.FirstOrDefault(a => a.Id == response.ServiceObject.RoleId);

				// Does the role exist in the server?
				if (guildRole != null)
				{
					// Add role to user
					await guildUser.RemoveRoleAsync((IRole)guildRole);
					await ReplyAsync("Role relieved!");
					return;
				}
				else
				{
					// TODO Delete the role
				}

			}
			await ReplyAsync("Couldn't find that role");
		}

		[Command("create")]
		[Alias("add", "new")]
		[RequireUserPermission(GuildPermission.ManageRoles)]
		[RequireBotPermission(GuildPermission.ManageRoles)]
		public async Task RoleCreateNewAsync(IRole role, string displayName = null)
		{
			if (displayName == null)
			{
				// Allow fallback to the role name
				displayName = role.Name;
			}

			var guildCheck = await _guildService.GetGuildAsync(Context.Guild.Id);
			if (!guildCheck.Success)
			{
				// Setup the guild
				var guildDb = await _guildService.CreateGuildAsync(Context.Guild.Id);
				if (!guildDb.Success)
				{
					// Couldn't register the guild
					// TODO Add a better logging method
					await ReplyAsync("Failed to register guild in our system.");
					return;
				}
			}

			// Create the role in the db
			var response = await _guildService.CreateGuildRoleAsync(Context.Guild.Id, role.Id, displayName);
			if (response.Success)
			{
				// Created role
				await ReplyAsync($"Bound the role `{role.Name}` to `{displayName}`. To get this role, run `!role apply {displayName}`!");
				return;
			}
			await ReplyAsync("Failed to create the role in our system.");
		}
		#endregion
	}
}
