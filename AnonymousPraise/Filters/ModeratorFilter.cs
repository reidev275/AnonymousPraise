using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace AnonymousPraise.Filters
{
	public class ModeratorFilter : System.Web.Http.Filters.AuthorizationFilterAttribute
	{
		public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			base.OnAuthorization(actionContext);

			var result = actionContext.Request.IsModerator();
			if (!result) throw new UnauthorizedAccessException();
		}
	}

	public static class ApiExtensions
	{
		private static readonly string ModeratorKey = ConfigurationManager.AppSettings["moderatorKey"];

		public static bool IsModerator(this HttpRequestMessage request)
		{
			IEnumerable<string> enumerableHeader;
			request.Headers.TryGetValues("praisemoderator", out enumerableHeader);
			if (enumerableHeader == null) return false;

			var list = enumerableHeader as IList<string> ?? enumerableHeader.ToList();
			if (list.Count == 0) return false;

			var csAuthString = list.First();
			return csAuthString == ModeratorKey;
		}
	}
}