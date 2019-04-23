using Discord;
using Discord.Commands;
using Jelli.ConsoleApp.Types;
using Jelli.Data.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jelli.ConsoleApp.Modules.Basic
{
	[Group("role")]
	[RequireContext(ContextType.Guild)]
	public class RoleModule : ModuleBase<SocketCommandContext>
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
		public async Task RoleApplyAsync(string displayName)
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

		[RequireBotPermission(GuildPermission.ManageRoles)]
		[Command("relieve")]
		[Alias("revoke", "remove")]
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
		[RequireUserPermission(GuildPermission.ManageRoles)]
		[RequireBotPermission(GuildPermission.ManageRoles)]
		public async Task RoleCreateAsync(string roleName, string displayName = null)
		{
			var matchingRoles = Context.Guild.Roles.Where(a => a.Name == roleName);
			if (matchingRoles.Count() > 1)
			{
				await ReplyAsync("Found too many matching roles.");
				return;
			}
			if (!matchingRoles.Any())
			{
				await ReplyAsync("No matching roles found.");
				return;
			}
			var matchedRole = matchingRoles.First();

			if (displayName == null)
			{
				// Allow fallback to the role name
				displayName = roleName;
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
			var response = await _guildService.CreateGuildRoleAsync(Context.Guild.Id, matchedRole.Id, displayName);
			if (response.Success)
			{
				// Created role
				await ReplyAsync($"Bound the role `{roleName}` to `{displayName}`. To get this role, run `!role apply {displayName}`!");
				return;
			}
			await ReplyAsync("Failed to create the role in our system.");
		}
		#endregion
	}
}
