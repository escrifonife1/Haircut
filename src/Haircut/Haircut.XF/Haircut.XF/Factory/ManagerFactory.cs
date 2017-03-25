using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SimpleInjector;
using Haircut.Core.Contract;
using Haircut.Core.Services;

namespace Haircut.Droid.Resources.Factory
{
    public class ManagerFactory
    {
        private static Container container;
        
        private static void Initialize()
        {
            if (container != null)
            {
                return;
            }

            container = new Container();

            container.Register<ILoginService, LoginService>();
            container.Register<ISchedulesService, SchedulesService>();
            container.Register<IHairdresserService, HairdresserService>();
            container.Register<IBarbershoperService, BarbershopService>();

            container.Verify();            
        }

        public static T GetInstance<T>() where T : class
        {
            Initialize();
            return container.GetInstance<T>();
        }
    }
}
