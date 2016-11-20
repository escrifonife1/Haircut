
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

namespace Haircut.Android
{
	[Activity(Label = "Login")]
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Login);

            var button_confirm = FindViewById<Button>(Resource.Id.button_confirm);
            var editText_userName = FindViewById<EditText>(Resource.Id.editText_userName);
            var editText_passWord = FindViewById<EditText>(Resource.Id.editText_passWorl);

            button_confirm.Click += (s, e) =>
            {
                var loginService = new LoginService();
                var login = loginService.Log(new Login()
                {
                    UserName = editText_userName.Text,
                    Password = editText_passWord.Text
                });

                if(login != null)
                {

                }
            };
		}
    }
}
