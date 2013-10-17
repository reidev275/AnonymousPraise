using AnonymousPraise.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousPraise.Data
{
	public class MemoryPeopleRepository : IPeopleRepository
	{
		static readonly List<Person> _people = new List<Person>
		{
			new Person
			{
				Email = "reievans@corelogic.com",
				Id = 0,
				Name = "Reid Evans"
			}
		};

		public IEnumerable<string> GetAll()
		{
			return _people.Select(x => x.Name);
		}

		public Person Get(string person)
		{
			return _people.FirstOrDefault(x => x.Name == person);
		}
	}
}