using System.Collections.Generic;

namespace AnonymousPraise.Data
{
	public class MemoryPeopleRepository : IPeopleRepository
	{
		public IEnumerable<string> GetAll()
		{
			return new [] 
			{ 
				"Adrienne Giles", 
				"Amy Holt",
				"April Roberts",
				"Brent Boutwell", 
				"Brent Crabtree",
				"Brian Honaker",
				"Chad Hughes",
				"Chris Dotson",
				"Chris Pecoraro",
				"Cody Kirkland", 
				"Daniel Pitz",
				"Daniel West",
				"Dyana Clement",
				"Eric Branch",
				"Eric Long",
				"Heather Nixon",
				"Jason Hurst",
				"Jay Witherspoon",
				"Jennifer Holladay",
				"John Copelan",
				"John Fields",
				"John Kinlaw",
				"Jonathan Kemp",
				"JT Thome",
				"Justin Bornhoeft",
				"Justin Jarmon",
				"Kathleen Popham",
				"Kim Herrington",
				"Matt Borgers",
				"Matthew Taylor",
				"Matthew Yost",
				"Michael Combest",
				"Nathan Kelley",
				"Nathaniel Morris",
				"Patrick Hayes",
				"Phil Pare",
				"Reid Evans",
				"Roberta Majors",
				"Steve Barbour",
				"Tiffani Waller",
				"Tim Lucas",
				"Trevor Valentine",
				"William Goley",
				"William West"
			};
		}
	}
}