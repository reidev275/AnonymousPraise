[assembly: WebActivator.PreApplicationStartMethod(typeof(ImageQueue.Service.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(ImageQueue.Service.NinjectWebCommon), "Stop")]

namespace ImageQueue.Service
{
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;
	using Ninject.Web.Common;

	public static class NinjectWebCommon 
	{
		public static IBootstrapper Bootstrapper = new Bootstrapper();

		public static void Start() 
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			var kernelCreator = new KernelCreator();
			Bootstrapper.Initialize(kernelCreator.CreateKernel);
		}

		public static void Stop()
		{
			Bootstrapper.ShutDown();
		}
	}
}