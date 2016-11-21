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
using Android.Support.V4.App;
using System.Threading.Tasks;
using Android.Content.PM;
using Plugin.Permissions;
using Humanizer;

namespace Haircut.Droid.Resources.Activitys
{
    public abstract class ActivityPermissionBase : ActivityBase, ActivityCompat.IOnRequestPermissionsResultCallback
    {
        void ActivityCompat.IOnRequestPermissionsResultCallback.OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
                
        public async Task<bool> IsPermissionGranted(Plugin.Permissions.Abstractions.Permission permission)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

			if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
			{
				var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { permission });
				status = results[Plugin.Permissions.Abstractions.Permission.Camera];

				if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
					{
						Toast.MakeText(this, $"Permissão <b>{permission.Humanize(LetterCasing.Title)}</b> necessária para continuar!", ToastLength.Long).Show();
					}
				}
			}

            return status == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
        }
    }
}