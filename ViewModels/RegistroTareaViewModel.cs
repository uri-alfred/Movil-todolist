using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;
using TodoList.Models.Encuestas;
using TodoList.Pages;
using TodoList.Services;

namespace TodoList.ViewModels
{
    public partial class RegistroTareaViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private Tarea tarea;

        private IDataService fakeService;

                                                    //puede definirse a este nivel o en el constructor
        public string[] TiposTareas { get; set; } = (string[])Enum.GetNames(typeof(eTipoTarea));
        public string[] Prioridad { get; set; } = (string[])Enum.GetNames(typeof(ePrioridad));
        public string[] Estado { get; set; } = (string[])Enum.GetNames(typeof(eEstado));

        public RegistroTareaViewModel(IDataService service)
        {
            tarea = new Tarea();
            fakeService = service;
        }

        [RelayCommand]
        private void Guardar()
        {
            fakeService.AddTask(Tarea);
            Shell.Current.GoToAsync("..");
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            object value = null;
            if (query.TryGetValue("TAREA", out value))
            {
                Tarea = value as Tarea;
            }
            if (query.TryGetValue("ENCUESTA", out value))
            {
                Tarea.Encuesta = value as Encuesta;
            }
        }

        [RelayCommand]
        public void AbrirRegistroEncuesta()
        {
            Shell.Current.GoToAsync(nameof(RegistroEncuestaPage));
        }

    }
}
