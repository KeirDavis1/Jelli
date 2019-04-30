using System;

namespace Jelli.Data.Types
{
	public enum EEnforcementType
	{
		#region Enums
		Unknown,
		RestrictPictures,
		RestrictText,
		MinimumGuildJoinedAgeDays,
		MinimumDiscordAgeDays,
		MinimumCharacters
		#endregion
	}

	public static class EnforcementTypeExtensions
	{

		public static EEnforcementType FromStringToEnforcementType(this string type)
		{
			EEnforcementType output;
			try
			{
				EEnforcementType.TryParse(type, true, out output);
				return output;
			}
			catch (ArgumentException)
			{
				return EEnforcementType.Unknown;
			}
		}
	}
}