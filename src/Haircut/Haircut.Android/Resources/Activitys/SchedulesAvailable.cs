
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
using Android;
using System.Threading.Tasks;

namespace Haircut.Droid.Resources.Activitys
{
	[Activity(Label = "Horarios Disponiveis")]
	public class SchedulesAvailable : ActivityPermissionBase
	{
        private ListView _horariosDisponiveis;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SchedulesAvailable);

            _horariosDisponiveis = FindViewById<ListView>(Resource.Id.listViewHorarios);
            _horariosDisponiveis.ItemClick += _horariosDisponiveis_ItemClick;
		}

        private void _horariosDisponiveis_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var horario =(string) _horariosDisponiveis.GetItemAtPosition(e.Position);

            Toast.MakeText(this, horario, ToastLength.Long).Show();
        }

        protected async override void OnResume()
        {
            base.OnResume();

            await MakeRequestAsync(async () =>
            {
                var horariosDisponiveisService = ManagerFactory.GetInstance<ISchedulesService>();
                var horariosDisponiveis = await horariosDisponiveisService.Disponiveis();
                _horariosDisponiveis.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, horariosDisponiveis.ToArray());
            });
        }


    }
}
