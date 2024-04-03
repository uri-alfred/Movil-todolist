using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [ObservableProperty]
        private string tituloPage;

        [ObservableProperty]
        private bool isActivo;

        [ObservableProperty]
        private bool isConfigurable;

        [ObservableProperty]
        private bool isSelectOtherFile;

        [ObservableProperty]
        private string archivoSeleccionado;

        private bool isEditar { get; set; } = false;
        public string fileName { get; set; }
        private bool siGuardo { get; set; }

        private IDataService fakeService;
        private IStorageService storageService;

                                                    //puede definirse a este nivel o en el constructor
        public string[] TiposTareas { get; set; } = (string[])Enum.GetNames(typeof(eTipoTarea));
        public string[] Prioridad { get; set; } = (string[])Enum.GetNames(typeof(ePrioridad));
        public string[] Estado { get; set; } = (string[])Enum.GetNames(typeof(eEstado));

        public RegistroTareaViewModel(IDataService service, IStorageService storageService)
        {
            tarea = new Tarea();
            fakeService = service;
            this.storageService = storageService;
            TituloPage = "Nueva Tarea";
            isActivo = false;
            isConfigurable = true;
            siGuardo = false;
            isSelectOtherFile = true;
            archivoSeleccionado = string.Empty;
        }

        [RelayCommand]
        private async void Guardar()
        {
            if (Tarea.Titulo == null || Tarea.Descripcion == null ||
                string.Empty.Equals(Tarea.Titulo) || string.Empty.Equals(Tarea.Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El titulo y la descripción de tarea no deben estar vacios.", "Ok");
            } else
            {
                if (isEditar)
                {
                    fakeService.EditTaskAsync(Tarea);
                }
                else
                {
                    Tarea.Estado = eEstado.Activo;
                    await fakeService.AddTask(Tarea);
                }
                siGuardo = true;
                await Shell.Current.GoToAsync("..");

            }
            
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            object value = null;
            if (query.TryGetValue("TAREA", out value))
            {
                Tarea = value as Tarea;
                TituloPage = "Editar tarea";
                isEditar = true;
                ArchivoSeleccionado = Tarea.NombreArchivo;
                if (Tarea.Estado == eEstado.Activo)
                {
                    IsActivo = true;
                }
                if (Tarea.NombreArchivo != null || !string.Empty.Equals(Tarea.NombreArchivo))
                {
                    IsSelectOtherFile = false;
                }
                if (Tarea.Estado == eEstado.Completado || Tarea.Estado == eEstado.Cancelado)
                {
                    IsConfigurable = false;
                    IsSelectOtherFile = false;
                }
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
        
        [RelayCommand]
        public async Task Cancelar()
        {
            Tarea.Estado = eEstado.Cancelado;
            await fakeService.EditTaskAsync(Tarea);
            _ = Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async void SeleccionarArchivo()
        {
            
            var file = await FilePicker.PickAsync();
            if (file != null)
            {
                // Subir el archivo a Firebase Storage
                using (var stream = await file.OpenReadAsync())
                {
                    fileName = $"archivo_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";

                    // Asignar la URL de descarga al campo URL de la tarea
                    Tarea.URL = await storageService.UploadFile(stream, fileName);
                    Tarea.NombreArchivo = fileName;
                    ArchivoSeleccionado = fileName;
                    IsSelectOtherFile = false;
                }
            }
        }

        public async Task EliminarArchivo()
        {
            // si no se dio a guardar cambios
            if(!siGuardo)
            {
                // Llamar al método para eliminar el archivo
                await storageService.DeleteFile(fileName);
            }

        }


    }
}
