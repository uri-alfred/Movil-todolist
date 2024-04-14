using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Plugin.ValidationRules;
using TodoList.Models;
using TodoList.Pages;

namespace TodoList.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _client;

        [ObservableProperty]
        private Login lgin;

        public LoginViewModel(FirebaseAuthClient client)
        {
            _client = client;
            lgin = new Login();
        }

        [RelayCommand]
        public async void Login()
        {
            try
            {
                var unit = new ValidationUnit(Lgin.email, Lgin.password);
                if (unit.Validate())
                {
                    var user = await _client.SignInWithEmailAndPasswordAsync(Lgin.email.Value, Lgin.password.Value);
                    Lgin = new Login();
                    _ = Shell.Current.GoToAsync(nameof(ToDoPage));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error al intentar iniciar sesión, intenta de nuevo más tarde.", "Ok");
            }
            
        }
        [RelayCommand]
        public void RegistroPage()
        {
            Shell.Current.GoToAsync(nameof(RegistroPage));
        }
    }
}
