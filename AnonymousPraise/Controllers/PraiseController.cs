using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using AnonymousPraise.Data;
using AnonymousPraise.Filters;
using AnonymousPraise.Models;

namespace AnonymousPraise.Controllers
{
    public class PraiseController : ApiController
    {
    	private readonly IPraiseRepository _praiseRepository;

    	public PraiseController()
    	{
			_praiseRepository = new SqlPraiseRepository(ConfigurationManager.ConnectionStrings["praise"].ConnectionString);
    	}

        // GET api/praise
        public IEnumerable<Praise> Get(string name = null, bool moderated = true, int offset = 0, int count = 20)
        {
			if (!String.IsNullOrEmpty(name)) return _praiseRepository.GetByPerson(name);
			if (!moderated)
			{
				return Request.IsModerator() ? _praiseRepository.GetUnmoderated() : new List<Praise>();
			}
			return _praiseRepository.GetAll().OrderByDescending(x => x.CreatedDate).Take(count);
        }

        // GET api/praise/Reid Evans
		public IEnumerable<Praise> Get(string id, int offset = 0, int count = 20)
        {
			return _praiseRepository.GetByPerson(id);
        }

        // POST api/praise
        public Praise Post([FromBody]Praise praise)
        {
        	praise.CreatedDate = DateTime.Now;
        	var id = _praiseRepository.Add(praise);
			praise.Id = id;
	        var moderator = Request.IsModerator();
			return moderator ? praise : null;
        }

		// DELETE api/praise
		[ModeratorFilter]
		public void Delete(int id)
		{
			_praiseRepository.Delete(id);
		}

		[ModeratorFilter]
		public void Put(int id, bool moderated = false)
		{
			if (moderated)
				_praiseRepository.MarkModerated(id);
		}
    }
}
