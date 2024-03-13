using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;
using TodoList.Pages;
using TodoList.Services;

namespace TodoList.ViewModels
{
    public partial class TodoviewModel : ObservableObject
    {
        public ObservableCollection<Tarea> Tareas { get; set; }
        private IDataService fakeService;

        [ObservableProperty]
        private Tarea tareaSeleccionada;

        [ObservableProperty]
        private bool isRefresh;

        public TodoviewModel(IDataService service)
        {
            Tareas = new ();
            fakeService = service;
        }

        [RelayCommand]
        public void AgregarTareas()
        {
            IsRefresh = true;
            RefreshItems();
            IsRefresh = false;
        }

        void RefreshItems()
        {
            Tareas.Clear();
            fakeService.Tasks.ForEach(Tareas.Add);
        }

        [RelayCommand]
        public void AbrirRegistro()
        {
            Shell.Current.GoToAsync(nameof(RegistroTareaPage));
        }

        [RelayCommand]
        public void EditarRegistro()
        {
            if (TareaSeleccionada == null)
            {
                return;
            }

            ShellNavigationQueryParameters parametros = new()
            {
                { "TAREA", TareaSeleccionada }
            };

            Shell.Current.GoToAsync(nameof(RegistroTareaPage), parametros);
        }
    }
}
