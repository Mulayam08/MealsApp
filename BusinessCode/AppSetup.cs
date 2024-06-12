using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CommunityToolkit.Mvvm.DependencyInjection;
using MealApp.ApiProviders;

namespace MealApp.BusinessCode
{
    public class AppSetup
    {
        public Autofac.IContainer CreateContainer()
        {
            ContainerBuilder cb = new ContainerBuilder();
            RegisterDepenencies(cb);
            return cb.Build();
        }

        protected virtual void RegisterDepenencies(ContainerBuilder cb)
        {
            Ioc.Default.ConfigureServices(
            new ServiceCollection()
            .AddSingleton<IApiProvider, ApiProvider>()
            .AddSingleton<IBusinessCode, BuisnessCode>()   
            .BuildServiceProvider());
        }
    }
}
