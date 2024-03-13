using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using TodoList.Pages;

namespace TodoList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // Binding
        [ObservableProperty]
        private string mensaje = "Click";

        private int count;
        public MainViewModel() { 
            

        }

        //Binding
        [RelayCommand]
        private void Increment()
        {
            count++;
            Mensaje = "Clicks " + count;

        }
        [RelayCommand]
        private void StartApp()
        {
            Shell.Current.GoToAsync(nameof(ToDoPage));
        }
        [RelayCommand]
        private void Login()
        {
            Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
