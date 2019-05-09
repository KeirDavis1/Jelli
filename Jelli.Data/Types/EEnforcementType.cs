using System;

namespace Jelli.Data.Types
{
	public enum EEnforcementType
	{
		#region Enums
		Unknown,
		RestrictPictures,
		RestrictText,
		RequiresText,
		RequiresPictures,
		MinimumGuildJoinedAgeDays,
		MinimumDiscordAgeDays,
		MinimumCharacters
		#endregion
	}

	public static class EnforcementTypeExtensions
	{

		public static EEnforcementType FromStringToEnforcementType(this string type)
		{
			try
			{
				EEnforcementType.TryParse(type, true, out EEnforcementType output);
				return output;
			}
			catch (ArgumentException)
			{
				return EEnforcementType.Unknown;
			}
		}
	}
}