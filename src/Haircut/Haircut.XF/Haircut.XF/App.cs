using Haircut.Core.Contract;
using Haircut.Droid.Resources.Factory;
using Haircut.Model.Models;
using Haircut.XF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Haircut.XF
{
    public class App : Application
    {
        public App()
        {
            var loginPage = new LoginPage();
            loginPage.ButtonLoginClick += async (userLogin, userPassword) =>
            {
                var loginService = ManagerFactory.GetInstance<ILoginService>();

                var login = await loginService.Log(new Login()
                {
                    UserName = userLogin.Text,
                    Password = userPassword.Text
                });

                System.Diagnostics.Debug.WriteLine(login);

                //ValidateServiceAndContinue(loginService, () =>
                //{
                //    Settings.Local.Set("login", login);

                //    StartActivity(typeof(SchedulesAvailableActivity));
                //});
            };

            MainPage = new NavigationPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
