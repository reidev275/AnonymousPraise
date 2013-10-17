using AnonymousPraise.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousPraise.Data
{
	public class MemoryModerationsRepository : IModerationsRepository
	{
		readonly static List<Moderation> _moderations = new List<Moderation>();

		public void Insert(Moderation moderation)
		{
			_moderations.Add(moderation);
		}

		public Moderation GetByGuid(string guid)
		{
			return _moderations.FirstOrDefault(x => x.Guid == guid);
		}


		public void Delete(int id)
		{
			//throw new System.NotImplementedException();
		}

		public void MarkModerated(int id)
		{
			//throw new System.NotImplementedException();
		}
	}
}