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
using System.Threading.Tasks;
using Humanizer;

namespace Haircut.Droid.Resources.Activitys
{
    public abstract class ActivityBase : Activity
    {
        private ProgressDialog _progressDialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetTheme(Android.Resource.Style.ThemeMaterial);
            //SetTheme(Android.Resource.Style.ThemeMaterialDialogWhenLarge);
            //SetTheme(Android.Resource.Style.ThemeMaterialWallpaper);
            _progressDialog = new ProgressDialog(this);
        }

        public void ShowDialogFragment(DialogFragment fragment)
        {
            var transaction = FragmentManager.BeginTransaction();
            fragment.Show(transaction, fragment.GetType().Name);
        }


        public async Task MakeRequestAsync(Action func, bool mostrarMsgProcesando = true)
        {
            await MakeRequestAsync(() => Task.Run(func), mostrarMsgProcesando);
            int abc = 2;
            System.Diagnostics.Debug.WriteLine(abc);
        }

        public async Task MakeRequestAsync(Func<Task> func, bool mostrarMsgProcesando = true)
        {
            var taskFunc = func();

            try
            {
                if (!_progressDialog.IsShowing)
                {
                    if (mostrarMsgProcesando)
                    {
                        _progressDialog.SetMessage("Aguarde...");
                        _progressDialog.SetTitle("Aguarde, processando...");
                        _progressDialog.SetCancelable(false);
                        _progressDialog.Indeterminate = true;
                        _progressDialog.Show();
                    }
                }

                await taskFunc;
            }
            /*catch (ExceptionFromApi ex)
            {
                var ab = new AlertDialog.Builder(this);
                ab.SetMessage(ex.Message)
                .SetPositiveButton("Ok", (oo, ee) => { });
                RunOnUiThread(() => ab.Show());
            }*/
            catch (Exception ex)
            {
                //Insights.Report(ex, Insights.Severity.Critical);

                var ab = new AlertDialog.Builder(this);
                ab.SetMessage($"Aconteceu um erro ao executar essa a��o!\n\n{ex.Message}\n\nDeseja tentar novamente?")
                .SetPositiveButton("Sim", async (oo, ee) =>
                {
                    await MakeRequestAsync(func, mostrarMsgProcesando);
                })
                .SetNegativeButton("N�o", (ooo, eee) => { });
                RunOnUiThread(() => ab.Show());
            }
            finally
            {
                if (mostrarMsgProcesando)
                    _progressDialog.Dismiss();
            }
        }
    }
    
}