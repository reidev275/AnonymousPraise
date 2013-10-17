using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace ImageQueue.Service
{
	public class KernelCreator
	{
		private readonly IKernel _kernel;

		public KernelCreator() : this(new StandardKernel()) {}

		public KernelCreator(IKernel kernel)
		{
			_kernel = kernel;
		}

		public IKernel CreateKernel()
		{
			_kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			_kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
			var serviceModule = new ServiceModule();
			_kernel.Load(serviceModule);

			GlobalConfiguration.Configuration.Services.Replace(
				typeof(IHttpControllerActivator),
				new NinjectKernelActivator(_kernel));
			return _kernel;
		}
	}
}