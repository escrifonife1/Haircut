using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimpleInjector;
using Haircut.Core.Contract;
using Haircut.Core.Services;

namespace Haircut.Android.Resources.Factory
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

            container.Verify();            
        }

        public static T GetInstance<T>() where T : class
        {
            Initialize();
            return container.GetInstance<T>();
        }
    }
}
