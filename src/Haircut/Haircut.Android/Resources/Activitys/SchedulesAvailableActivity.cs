
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
	public class SchedulesAvailableActivity : ActivityPermissionBase
	{
        private ListView _listView_horariosDisponiveis;
        private List<Schedule> _schedulesAvailables;
        private ISchedulesService _scheduleService;
        Login _login;
        private Spinner _spinner_barbershop, _spinner_hairdresser;
        private IBarbershoperService _barbershopService;
        private IHairdresserService _hairdresserService;
        private List<BarberShop> _barbershopers;
        private List<Hairdresser> _hairdressers;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SchedulesAvailable);

            _login = Settings.Local.Get<Login>("login");
            _scheduleService = ManagerFactory.GetInstance<ISchedulesService>();            
            _barbershopService = ManagerFactory.GetInstance<IBarbershoperService>();
            _hairdresserService = ManagerFactory.GetInstance<IHairdresserService>();

            _listView_horariosDisponiveis = FindViewById<ListView>(Resource.Id.listViewHorarios);
            _listView_horariosDisponiveis.ItemClick += _horariosDisponiveis_ItemClick;
            _spinner_barbershop = FindViewById<Spinner>(Resource.Id.spinner_barbershop);
            _spinner_hairdresser = FindViewById<Spinner>(Resource.Id.spinner_haridresser);
            _spinner_barbershop.Prompt = "Barbearia";
            
            _spinner_hairdresser.Prompt = "Cabelereiro(a)";
            _spinner_hairdresser.ItemSelected += _spinner_hairdresser_ItemSelected;            
        }

        protected async override void OnResume()
        {
            base.OnResume();
            await MakeRequestAsync(async () =>
            {
                _barbershopers = await _barbershopService.Barbershopers();

                ValidateServiceAndContinue(_barbershopService, () =>
                {
                    var barbershopAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, _barbershopers.Select(b => b.Name).ToArray());
                    _spinner_barbershop.Adapter = barbershopAdapter;
                    _spinner_barbershop.ItemSelected += _spinner_barbershop_ItemSelected;
                });
            });
        }

        private async void _spinner_hairdresser_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            await MakeRequestAsync(async () =>
            {
                var spinner = (Spinner)sender;
                var toast = string.Format("Cabelereiro {0}", spinner.GetItemAtPosition(e.Position));
                Toast.MakeText(this, toast, ToastLength.Long).Show();
                await SchedulesAvailables();
            });
        }

        private async void _spinner_barbershop_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            await MakeRequestAsync(async () =>
            {
                var spinner = (Spinner)sender;

                var toast = string.Format("Barbearia {0}", spinner.GetItemAtPosition(e.Position));
                Toast.MakeText(this, toast, ToastLength.Long).Show();

                var barbershop = _barbershopers.ElementAt(e.Position);

                _hairdressers = await _hairdresserService.Hairdressers(barbershop);
                ValidateServiceAndContinue(_hairdresserService, () =>
                {
                    var hairdresserAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, _hairdressers.Select(h => h.Name).ToList());
                    _spinner_hairdresser.Adapter = hairdresserAdapter;
                });
            });
        }

        private void _horariosDisponiveis_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var schedule = _schedulesAvailables.ElementAt(e.Position);

            Func<int, string, Task> FuncSchedule = async (available, message) =>
            {
                schedule.LoginId = _login.Id;
                schedule.Available = available;
                schedule.Login = _login;
                await _scheduleService.Schedule(schedule);

                ValidateServiceAndContinue(_scheduleService, () =>
                {
                    Toast.MakeText(this, message, ToastLength.Long).Show();
                });

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
