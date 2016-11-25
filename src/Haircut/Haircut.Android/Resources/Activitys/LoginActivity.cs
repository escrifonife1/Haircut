
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
using Haircut.Core.Services;
using Haircut.Model.Models;
using Haircut.Droid.Resources.Factory;
using Haircut.Core.Contract;
using Acr.Settings;

namespace Haircut.Droid.Resources.Activitys
{
	[Activity(Label = "Login")]
	public class LoginActivity : ActivityPermissionBase
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Login);

            var button_confirm = FindViewById<Button>(Resource.Id.button_confirm);
            var editText_userName = FindViewById<EditText>(Resource.Id.editText_userName);
            var editText_passWord = FindViewById<EditText>(Resource.Id.editText_passWorl);
            var button_register = FindViewById<Button>(Resource.Id.button_register);

            button_register.Click += (s, e) =>
            {
                StartActivity(typeof(RegisterActivity)); 
            };

            button_confirm.Click += async (s, e) =>
            {
                //await IsPermissionGranted(Plugin.Permissions.Abstractions.Permission.Camera);
                await MakeRequestAsync(async () =>
                {
                    var loginService = ManagerFactory.GetInstance<ILoginService>();

                    var login = await loginService.Log(new Login()
                    {
                        UserName = editText_userName.Text,
                        Password = editText_passWord.Text
                    });


                    ValidateServiceAndContinue(loginService, () =>
                    {
                        Settings.Local.Set("login", login);

                        StartActivity(typeof(SchedulesAvailableActivity));
                    });                    
                });
            };
		}        
    }
}
