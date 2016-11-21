
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
                    
                    if (login?.Id > 0)
                    {
                        StartActivity(typeof(HorariosDisponiveisActivity));
                    }
                    else
                    {
                        Toast.MakeText(this, "Usuário ou senha incorreto!", ToastLength.Long).Show();
                    }                    
                });
            };
		}        
    }
}
