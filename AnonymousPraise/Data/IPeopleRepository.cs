using AnonymousPraise.Models;
using System.Collections.Generic;

namespace AnonymousPraise.Data
{
	public interface IPeopleRepository
	{
		IEnumerable<string> GetAll();
		Person Get(string person);
	}
}