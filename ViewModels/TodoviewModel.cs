using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        async Task RefreshItems()
        {
            // Obtener las tareas actualizadas del servicio
            List<Tarea> nuevasTareas = await fakeService.GetTasks();

            // Limpiar la lista actual de tareas en el ViewModel
            Tareas.Clear();

            // Agregar las nuevas tareas a la lista en el ViewModel
            foreach (var tarea in nuevasTareas)
            {
                Tareas.Add(tarea);
            }
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

        [RelayCommand]
        public async Task RemoveTask(Tarea tarea)
        {
            if (tarea == null)
            {
                return;
            }

            // Eliminar la tarea de Firebase
            bool deletedFromFirebase = await fakeService.DeleteTaskAsync(tarea);

            if (deletedFromFirebase)
            {
                // Si la tarea se eliminó correctamente de Firebase, también la eliminamos de nuestra colección local
                Tareas.Remove(tarea);
            }
            else
            {
                // Manejar el caso en que la eliminación de la tarea de Firebase falla
                Debug.WriteLine("La eliminación de la tarea de Firebase falló.");
            }
        }

        [RelayCommand]
        private async Task TaskCompleted(Tarea tarea)
        {
            if(tarea == null)
            {
                return;
            }
            
            if (tarea.TipoTarea == eTipoTarea.Encuesta)
            {
                bool respondida = false;
                foreach (var preg in tarea.Encuesta.Preguntas)
                {
                    if (preg.Respuestas.Count > 0)
                    {
                        respondida = true;
                        break;
                    }
                }
                if (!respondida)
                {
                    ShellNavigationQueryParameters parametros = new()
                    {
                        { "TAREA_COMPLETE", tarea }
                    };

                    _ = Shell.Current.GoToAsync(nameof(FormEncuestaPage), parametros);
                }
            } else
            {
                await fakeService.EditTaskAsync(tarea);
            }
        }
    }
}
