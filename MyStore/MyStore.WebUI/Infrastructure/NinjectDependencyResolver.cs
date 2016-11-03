using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Domain.Abstract;
using MyStore.Domain.Concrete;

namespace MyStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {

        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParm) {
            kernel = kernelParm;
            AddBindings();
        
        }

        private void AddBindings()
        {
            kernel.Bind<IProductsRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings",emailSettings);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}