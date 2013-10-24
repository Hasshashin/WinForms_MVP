using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Kensington.Common;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using WinFormsMvp;

namespace WinFormsMvp
{
    public static class ContainerBootstrapper
    {
        private static WindsorContainer container;

        static ContainerBootstrapper()
        {
            container = new WindsorContainer();
        }

        public static WindsorContainer Container {get {return container;}}

        public static void Initialize(string uiAssemblyName)
        {
            container.Install(new LogInstaller());
            container.Register(Types.FromAssemblyNamed(uiAssemblyName)//"PropertyManagemnet.UI"
                       .BasedOn(typeof(IPresenter)));
            
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public static T Resolve<T>(object additionalArgumentsAsAnonymousType)
        {
            return (T) container.Resolve<T>(additionalArgumentsAsAnonymousType);
        }

        public static  object Resolve(Type type)
        {
            return container.Resolve(type);
        }
        
    }
}
