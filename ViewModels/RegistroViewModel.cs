using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Pages;

namespace TodoList.ViewModels
{
    public partial class RegistroViewModel : ObservableObject
    {
        [RelayCommand]
        public void Registro()
        {
            Shell.Current.GoToAsync(nameof(ToDoPage));
        }
        [RelayCommand]
        public void LoginPage()
        {
            Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
