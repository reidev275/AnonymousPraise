using AnonymousPraise.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousPraise.Data
{
	public class MemoryPraiseRepository : IPraiseRepository
	{
		static readonly List<Praise> _praise = new List<Praise>();
		static int _id = 0;

		public IEnumerable<Models.Praise> GetAll()
		{
			return _praise;
		}

		public IEnumerable<Models.Praise> GetByPerson(string person)
		{
			return _praise;
		}

		public int Add(Models.Praise praise)
		{
			praise.Id = _id++;
			_praise.Add(praise);
			return praise.Id;
		}

		public void Delete(int id)
		{
			//throw new NotImplementedException();
		}

		public void MarkModerated(int id)
		{
			//throw new NotImplementedException();
		}

		public void Like(int id)
		{
			//throw new NotImplementedException();
		}

		public IEnumerable<Models.Praise> GetUnmoderated()
		{
			return _praise;
		}




		public Praise GetById(int id)
		{
			return _praise.FirstOrDefault(x => x.Id == id);
		}
	}
}