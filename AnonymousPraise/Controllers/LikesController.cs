using System.Configuration;
using System.Web.Http;
using AnonymousPraise.Data;

namespace AnonymousPraise.Controllers
{
    public class LikesController : ApiController
    {
		private readonly IPraiseRepository _praiseRepository;

		public LikesController()
    	{
			_praiseRepository = new SqlPraiseRepository(ConfigurationManager.ConnectionStrings["praise"].ConnectionString);
    	}

        // POST api/likes
        public void Post(int praiseId)
        {
	        _praiseRepository.Like(praiseId);
        }
    }
}
