using System.Web.Http;

namespace AnonymousPraise.App_Start
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional, offset = 0, count = 10 }
			);

			config.Routes.MapHttpRoute(
				name: "LikePraise",
				routeTemplate: "api/praise/{praiseId}/likes",
				defaults: new
					{
						controller = "Likes",
						offset = 0, 
						count = 10
					}
			);

		}
	}
}
