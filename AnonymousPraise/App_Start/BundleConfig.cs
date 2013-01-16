using System.Web.Optimization;

namespace AnonymousPraise
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/js")
				.Include(
					"~/Scripts/lib/knockout-2.1.0.js",
					"~/Scripts/ko.slideVisible.js",
					"~/Scripts/lib/jquery.cookie.js",
					"~/Scripts/ap.people.js",
					"~/Scripts/ap.praise.js",
					"~/Scripts/ap.routing.js",
					"~/Scripts/ap.viewModel.js",
					"~/Scripts/ap.initialize.js",
					"~/Scripts/Bootstrap/js/bootstrap.min.js"
				));
		}
	}
}