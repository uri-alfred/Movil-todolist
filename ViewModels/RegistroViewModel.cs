using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.ValidationRules;
using TodoList.Models;
using TodoList.Pages;

namespace TodoList.ViewModels
{
    public partial class RegistroViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _client;
        
        [ObservableProperty]
        private Registro reg;

        public RegistroViewModel(FirebaseAuthClient client)
        {
            _client = client;
            reg = new Registro();
        }

        [RelayCommand]
        public async void Registro()
        {
            var unit = new ValidationUnit(Reg.Username, Reg.Email, Reg.Password, Reg.RepeatPassword);
            try
            {
                if(unit.Validate())
                {
                    await _client.CreateUserWithEmailAndPasswordAsync(Reg.Email.Value, Reg.Password.Value);
                    await _client.User.ChangeDisplayNameAsync(Reg.Username.Value);
                    await Application.Current.MainPage.DisplayAlert("Exitoso", "Usuario registrado correctamente", "Ok");
                    Reg = new Registro();
                    _ = Shell.Current.GoToAsync(nameof(ToDoPage));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el usuario, intenta de nuevo más tarde.", "Ok");
            }
        }

        [RelayCommand]
        public void InicioSesionPage()
        {
            Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
