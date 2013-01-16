using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using AnonymousPraise.Data;

namespace AnonymousPraise.Controllers
{
	public class PeopleController : ApiController
	{
		private readonly IPeopleRepository _peopleRepository;

		public PeopleController()
		{
			_peopleRepository = new SqlPeopleRepository(ConfigurationManager.ConnectionStrings["praise"].ConnectionString);
		}

		// GET api/Employees
		public IEnumerable<string> Get()
		{
			return _peopleRepository.GetAll();
		}
	}
}