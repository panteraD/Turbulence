using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace mainWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                new ResolveEventHandler(ResolveAssembly);

            // proceed starting app...
        }

        static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly parentAssembly = Assembly.GetExecutingAssembly();

            var name = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
            var resourceName = parentAssembly.GetManifestResourceNames()
                .First(s => s.EndsWith(name));

            using (Stream stream = parentAssembly.GetManifestResourceStream(resourceName))
            {
                byte[] block = new byte[stream.Length];
                stream.Read(block, 0, block.Length);
                return Assembly.Load(block);
            }
        }
    }
}
