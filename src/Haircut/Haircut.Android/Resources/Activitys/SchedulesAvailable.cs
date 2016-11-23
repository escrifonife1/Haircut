
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
using Acr.Settings;

namespace Haircut.Droid.Resources.Activitys
{
	[Activity(Label = "Horarios Disponiveis")]
	public class SchedulesAvailable : ActivityPermissionBase
	{
        private ListView _listView_horariosDisponiveis;
        private List<Schedule> _schedulesAvailables;
        private ISchedulesService _scheduleService;
        Login _login;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SchedulesAvailable);

            _listView_horariosDisponiveis = FindViewById<ListView>(Resource.Id.listViewHorarios);
            _listView_horariosDisponiveis.ItemClick += _horariosDisponiveis_ItemClick;

            _scheduleService = ManagerFactory.GetInstance<ISchedulesService>();
            _login = Settings.Local.Get<Login>("login");
        }

        private async void _horariosDisponiveis_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var horario = (string)_listView_horariosDisponiveis.GetItemAtPosition(e.Position);

            var schedule = _schedulesAvailables.ElementAt(e.Position);
            schedule.LoginId = _login.Id;
            schedule.Available = 0;
            schedule.Login = _login;
            await _scheduleService.Schedule(schedule);

            Toast.MakeText(this, horario, ToastLength.Long).Show();
        }

        protected async override void OnResume()
        {
            base.OnResume();

            await MakeRequestAsync(async () =>
            {                
                _schedulesAvailables = await _scheduleService.Availables(DateTime.Today.AddHours(-2), _login.Id);
                var schedulesAvailables = _schedulesAvailables.Select(s => s.Date.ToString("dd/MM/yyyy hh:mm")).ToArray();
                _listView_horariosDisponiveis.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, schedulesAvailables);
            });
        }
    }
}
