using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;

namespace ProviderConsoleApplication
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ILogSaveProvider>();
            var container = builder.Build();

        }  
    }
}
