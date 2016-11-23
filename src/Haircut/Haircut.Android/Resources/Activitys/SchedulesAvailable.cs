
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
using Haircut.Model.Models;

namespace Haircut.Droid.Resources.Activitys
{
	[Activity(Label = "Horarios Disponiveis")]
	public class SchedulesAvailable : ActivityPermissionBase
	{
        private ListView _listView_horariosDisponiveis;
        private List<Schedule> _horariosDisponiveis;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SchedulesAvailable);

            _listView_horariosDisponiveis = FindViewById<ListView>(Resource.Id.listViewHorarios);
            _listView_horariosDisponiveis.ItemClick += _horariosDisponiveis_ItemClick;
		}

        private void _horariosDisponiveis_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var horario = (string)_listView_horariosDisponiveis.GetItemAtPosition(e.Position);

            Toast.MakeText(this, horario, ToastLength.Long).Show();
        }

        protected async override void OnResume()
        {
            base.OnResume();

            await MakeRequestAsync(async () =>
            {
                var horariosDisponiveisService = ManagerFactory.GetInstance<ISchedulesService>();
                _horariosDisponiveis = await horariosDisponiveisService.Disponiveis(DateTime.Today.AddHours(-2));
                var horariosDisponiveis = _horariosDisponiveis.Select(s => s.Date.ToString("dd/MM/yyyy hh:mm")).ToArray();
                _listView_horariosDisponiveis.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, horariosDisponiveis);
            });
        }


    }
}
