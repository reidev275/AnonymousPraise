using AnonymousPraise.Data;
using AnonymousPraise.Models;
using AnonymousPraise.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AnonymousPraise.Controllers
{
	public class PraiseController : ApiController
	{
		private readonly IPraiseRepository _praiseRepository;
		private readonly IPeopleRepository _peopleRepository;
		private readonly INotifier _notifier;
		private readonly IModerationsRepository _moderationsRepository;

		public PraiseController(IPraiseRepository praiseRepository, IPeopleRepository peopleRepository, INotifier notifier, IModerationsRepository moderationsRepository)
		{
			_praiseRepository = praiseRepository;
			_peopleRepository = peopleRepository;
			_notifier = notifier;
			_moderationsRepository = moderationsRepository;
		}

		// GET api/praise
		public IEnumerable<Praise> Get(string name = null, int offset = 0, int count = 20)
		{
			if (!String.IsNullOrEmpty(name)) return _praiseRepository.GetByPerson(name);
			return _praiseRepository.GetAll().OrderByDescending(x => x.CreatedDate).Take(count);
		}

		// POST api/praise
		public void Post([FromBody]Praise praise)
		{
			praise.CreatedDate = DateTime.Now;
			var id = _praiseRepository.Add(praise);
			praise.Id = id;

			var person = _peopleRepository.Get(praise.Person);
			var moderationGuid = Guid.NewGuid();

			_moderationsRepository.Insert(new Moderation { Guid = moderationGuid.ToString(), PraiseId = id });

			if (person != null) 
				_notifier.Notify(person.Email, 
				"<p>Someone just gave you the following praise...</p>" +
				"<p>" + praise.Comment + "</p>" +
				"<p><a href='http://" + Request.RequestUri.Authority + "/moderations/edit/" + moderationGuid + "'>Approve or Decline this praise</a>" +
				"<br><br><p>*All praise must be approved by you before it is visible to others on the site</p>");
		}
	}
}
