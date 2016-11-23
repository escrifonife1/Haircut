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
using Haircut.Droid.Resources.Factory;
using Haircut.Core.Contract;
using Haircut.Model.Models;

namespace Haircut.Droid.Resources.Activitys
{
    [Activity(Label = "Cadastrar")]
    public class RegisterActivity : ActivityPermissionBase
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            var button_confirm = FindViewById<Button>(Resource.Id.button_register);
            var editText_name = FindViewById<EditText>(Resource.Id.editText_Name);
            var editText_userName = FindViewById<EditText>(Resource.Id.editText_userName);
            var editText_phone = FindViewById<EditText>(Resource.Id.editText_phone);
            var editText_passWord = FindViewById<EditText>(Resource.Id.editText_passWord);
            var editText_confirm_passWord = FindViewById<EditText>(Resource.Id.editText_confirm_passWord);

            button_confirm.Click += async (s, e) =>
            {
				await MakeRequestAsync(async () =>
				{
					if (editText_confirm_passWord.Text != editText_passWord.Text)
					{
						Toast.MakeText(this, "As senhas não podem serem diferentes!", ToastLength.Long).Show();
						editText_confirm_passWord.RequestFocus();
						return;
					}

					var login = new Login()
					{
						UserName = editText_userName.Text,
						Password = editText_passWord.Text,
						Created = DateTime.Now,
						Name = editText_name.Text,
						Phone = editText_phone.Text
					};

					var loginService = ManagerFactory.GetInstance<ILoginService>();

					if (!loginService.IsValideForRegister(login))
					{
						Toast.MakeText(this, loginService.ErrorMessage(), ToastLength.Long).Show();
						return;
					}

					login = await loginService.Register(login);

					if (login?.Id > 0)
					{
						Toast.MakeText(this, "Cadastro registrado com sucesso!", ToastLength.Long).Show();
						this.Finish();
					}
					else
					{
						Toast.MakeText(this, loginService.ErrorMessage(), ToastLength.Long).Show();
					}
				});
            };
        }        
    }
}