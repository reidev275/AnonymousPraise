using System.Collections.Generic;
using AnonymousPraise.Models;

namespace AnonymousPraise.Data
{
	public interface IPraiseRepository
	{
		IEnumerable<Praise> GetAll();
		IEnumerable<Praise> GetByPerson(string person);
		Praise GetById(int id);
		int Add(Praise praise);
		void Like(int id);
		IEnumerable<Praise> GetUnmoderated();
	}
}