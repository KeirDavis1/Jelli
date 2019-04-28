using System.Collections.Generic;
using System.Linq;
namespace Jelli.ConsoleApp.Types
{
	public static class PaginatorExtensions
	{
		public static List<string> TextToPages(this string text, int maxCharsPerPage = 1536)
		{
			var pages = new List<string>();
			var page = 0;

			var split = text.Split("\n"); // Get each line
			foreach (var item in split)
			{
				if (pages.ElementAtOrDefault(page) == null)
				{
					pages.Add(string.Empty);
				}

				pages[page] += item;

				if (pages[page].Length > maxCharsPerPage)
				{
					page++;
				}
				else
				{
					pages[page] += "\n";
				}
			}

			return pages;
		}
	}

}