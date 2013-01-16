using System.Collections.Generic;

namespace AnonymousPraise.Data
{
	public interface IPeopleRepository
	{
		IEnumerable<string> GetAll();
	}
}