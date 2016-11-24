﻿
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
            var schedule = _schedulesAvailables.ElementAt(e.Position);

            Func<int, string, Task> FuncSchedule = async (available, message) =>
            {
                schedule.LoginId = _login.Id;
                schedule.Available = available;
                schedule.Login = _login;
                await _scheduleService.Schedule(schedule);

				if (string.IsNullOrWhiteSpace(_scheduleService.ErrorMessage()))
				{
					Toast.MakeText(this, message, ToastLength.Long).Show();
				}
				else
				{
					Toast.MakeText(this, _scheduleService.ErrorMessage(), ToastLength.Long).Show();
				}
				await SchedulesAvailables();
            };

            var ab = new AlertDialog.Builder(this);
            if (schedule.LoginId == _login.Id)
            {                
                ab.SetMessage($"Desmarcar horário?")
                .SetPositiveButton("Sim", async (oo, ee) =>
                {
                    await MakeRequestAsync(async () => await FuncSchedule(1, "Horário desmarcado com sucesso!"));
                })
                .SetNegativeButton("Não", (ooo, eee) => { });
            }
            else
            {
                ab.SetMessage($"Marcar horário?")
                .SetPositiveButton("Sim", async (oo, ee) =>
                {
                    await MakeRequestAsync(async () => await FuncSchedule(0, "Horário marcado com sucesso!"));
                })
                .SetNegativeButton("Não", (ooo, eee) => { });
            }

			ab.Show();            
        }

        protected async override void OnResume()
        {
            base.OnResume();
            await SchedulesAvailables();
        }

        private async Task SchedulesAvailables()
        {
            await MakeRequestAsync(async () =>
            {
                _schedulesAvailables = await _scheduleService.Availables(DateTime.Today.AddHours(-2), _login.Id);
                var schedulesAvailables = _schedulesAvailables.Select(s =>
                {
                    if (s.LoginId == _login.Id)
                    {
                        return $"Seu horário {s.Date.ToString("dd /MM/yyyy hh:mm")}";
                    }
                    else
                    {
                        return $"Disponível {s.Date.ToString("dd /MM/yyyy hh:mm")}";
                    }
                }).ToArray();
                _listView_horariosDisponiveis.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, schedulesAvailables);
            });
        }
    }
}
