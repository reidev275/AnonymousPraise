using System.Collections.Generic;
using AnonymousPraise.Models;

namespace AnonymousPraise.Data
{
	public interface IPraiseRepository
	{
		IEnumerable<Praise> GetAll();
		IEnumerable<Praise> GetByPerson(string person);
		int Add(Praise praise);
		void Delete(int id);
		void Like(int id);

		IEnumerable<Praise> GetUnmoderated();
		void MarkModerated(int id);
	}
}