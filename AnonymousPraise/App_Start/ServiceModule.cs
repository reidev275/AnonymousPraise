using AnonymousPraise.Data;
using AnonymousPraise.Notification;
using Ninject.Modules;
using System.Configuration;

namespace ImageQueue.Service
{
	public class ServiceModule : NinjectModule
	{
		public override void Load()
		{
			var connectionString = ConfigurationManager.ConnectionStrings["anonymouspraise_db"].ConnectionString;
			var fromEmail = ConfigurationManager.AppSettings["fromEmail"];
			var emailPassword = ConfigurationManager.AppSettings["emailPw"];

			Bind<IPeopleRepository>().To<SqlPeopleRepository>().InSingletonScope().WithConstructorArgument("connectionString", connectionString);
			Bind<IPraiseRepository>().To<SqlPraiseRepository>().InSingletonScope().WithConstructorArgument("connectionString", connectionString);
			Bind<IModerationsRepository>().To<SqlModerationsRepository>().InSingletonScope().WithConstructorArgument("connectionString", connectionString);


			//Bind<IPraiseRepository>().To<MemoryPraiseRepository>();
			//Bind<IPeopleRepository>().To<MemoryPeopleRepository>();
			//Bind<IModerationsRepository>().To<MemoryModerationsRepository>();


			Bind<INotifier>().To<EmailNotifier>().InSingletonScope()
				.WithConstructorArgument("from", fromEmail)
				.WithConstructorArgument("password", emailPassword);
		}
	}
}