using AnonymousPraise.Data;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace AnonymousPraise.Controllers
{
	public class ModerationsController : Controller
	{
		readonly IModerationsRepository _moderationsRepository;
		readonly IPraiseRepository _praiseRepository;

		public ModerationsController()
		{
			var connectionString = ConfigurationManager.ConnectionStrings["anonymouspraise_db"].ConnectionString;

			_moderationsRepository = new SqlModerationsRepository(connectionString);
			_praiseRepository = new SqlPraiseRepository(connectionString);
		}

		//
		// GET: /Moderations/Edit/5

		public ActionResult Edit(string id)
		{
			var moderation = _moderationsRepository.GetByGuid(id);
			var praise = _praiseRepository.GetById(moderation.PraiseId);

			return View(praise);
		}

		//
		// POST: /Moderations/Edit/5
		[HttpPost]
		public ActionResult Edit(string id, FormCollection collection)
		{
			var praiseId = Convert.ToInt32(collection["praiseId"]);

			if (collection["Approve"] == "true")
			{
				_moderationsRepository.MarkModerated(praiseId);
			}
			else
			{
				_moderationsRepository.Delete(praiseId);
			}
			return Redirect("http://" + Request.Url.Authority);
		}
	}
}