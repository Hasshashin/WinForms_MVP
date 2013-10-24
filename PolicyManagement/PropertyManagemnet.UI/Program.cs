using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PropertyManagemnet.UI.Views;
using WinFormsMvp;
using Castle.MicroKernel.Registration;
using System.Reflection;

namespace PropertyManagemnet.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ContainerBootstrapper.Initialize(GetStartupProjectName());
            //ContainerBootstrapper.Container.Register(Component.For<IPolicyManagementView>().ImplementedBy<PolicyManagementForm>());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PolicyManagementForm());
        }

        private static string GetStartupProjectName()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(assembly.FullName);
            return assemblyName.Name;
        }
    }
}
