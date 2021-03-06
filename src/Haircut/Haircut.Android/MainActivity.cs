﻿using Android.App;
using Android.Widget;
using Android.OS;
using Haircut.Droid.Resources.Activitys;

namespace Haircut.Droid
{
    [Activity(Label = "Haircut", MainLauncher = true)]
    public class MainActivity : ActivityPermissionBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            var button = FindViewById<Button>(Resource.Id.button_test);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }
    }
}

