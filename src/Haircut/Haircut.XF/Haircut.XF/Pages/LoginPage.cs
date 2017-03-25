using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Haircut.XF.Pages
{
    public class LoginPage : ContentPage
    {

        public LoginPage()
        {
            Title = "Haircut.XF";

            var indicator = new ActivityIndicator()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Color = Color.Red,
                IsVisible = false
            };

            var userEntry = new Entry()
            {
                Placeholder = "Usuário",
                BackgroundColor = Color.Black,
                TextColor = Color.White
            };
            var passwordEntry = new Entry()
            {
                Placeholder = "Senha",
                BackgroundColor = Color.Black,
                TextColor = Color.White
            };

            var buttonLogin = new Button()
            {
                Text = "Login",
                BackgroundColor = Color.Blue,
                TextColor = Color.White
            };
            var buttonRegister = new Button()
            {
                Text = "Cadastrar",
                BackgroundColor = Color.Blue,
                TextColor = Color.White
            };

            Action<bool> ChangeIndicator = (isVisible) =>
            {
                indicator.IsVisible = isVisible;
                indicator.IsRunning = isVisible;
            };

            buttonLogin.Clicked += async (s, e) =>
            {
                ChangeIndicator(true);
                await ButtonLoginClick(userEntry, passwordEntry);
                ChangeIndicator(false);
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    indicator,
                    userEntry,
                    passwordEntry,
                    buttonLogin,
                    buttonRegister,
                    }
            };
        }

        public Func<Entry, Entry, Task> ButtonLoginClick { get; set; }
    }
}
