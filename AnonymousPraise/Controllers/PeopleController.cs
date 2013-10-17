using AnonymousPraise.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace AnonymousPraise.Controllers
{
	public class PeopleController : ApiController
	{
		private readonly IPeopleRepository _peopleRepository;

		public PeopleController(IPeopleRepository peopleRepository)
		{
			_peopleRepository = peopleRepository;
		}

		public IEnumerable<string> Get()
		{
			return _peopleRepository.GetAll();
		}
	}
}