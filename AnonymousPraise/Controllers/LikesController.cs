using AnonymousPraise.Data;
using System.Web.Http;

namespace AnonymousPraise.Controllers
{
	public class LikesController : ApiController
	{
		private readonly IPraiseRepository _praiseRepository;

		public LikesController(IPraiseRepository praiseRepository)
		{
			_praiseRepository = praiseRepository;
		}

		public void Post(int praiseId)
		{
			_praiseRepository.Like(praiseId);
		}
	}
}
