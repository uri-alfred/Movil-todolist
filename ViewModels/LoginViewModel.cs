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
    public partial class LoginViewModel : ObservableObject
    {
        [RelayCommand]
        public void Login()
        {
            Shell.Current.GoToAsync(nameof(ToDoPage));
        }
        [RelayCommand]
        public void RegistroPage()
        {
            Shell.Current.GoToAsync(nameof(RegistroPage));
        }
    }
}
