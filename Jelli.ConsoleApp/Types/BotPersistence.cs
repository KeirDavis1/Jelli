using System;

namespace Jelli.ConsoleApp.Types
{
	public class BotPersistence
	{
		#region Properties
		private DateTime startTime { get; set; }
		#endregion

		#region Constructor
		public BotPersistence()
		{
			this.startTime = DateTime.UtcNow;
		}
		#endregion

		#region Methods
		public TimeSpan GetUptime()
		{
			// Return the date difference between startup and now
			return DateTime.UtcNow - startTime;
		}
		#endregion
	}
}