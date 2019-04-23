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
		[Command("apply")]
		public Task RoleApplyAsync()
		{
			return ReplyAsync("You're appling for a role!");
		}

		[Command("create")]
		public Task RoleCreateAsync(string roleName, string displayName)
		{
			var matchingRoles = Context.Guild.Roles.Where(a => a.Name == roleName);
			if (matchingRoles.Count() > 1)
			{
				return ReplyAsync("Found too many matching roles.");
			}
			if (!matchingRoles.Any())
			{
				return ReplyAsync("No matching roles found.");
			}
			var matchedRole = matchingRoles.First();

			_guildService.CreateGuildRoleAsync(Context.Guild.Id, matchedRole.Id, displayName);
			return ReplyAsync("You're applying for a role!");
		}
		#endregion
	}
}
