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
        private bool isComplete;

        [ObservableProperty]
        private bool isSelectOtherFile;

        [ObservableProperty]
        private string archivoSeleccionado;

        private bool isEditar { get; set; } = false;
        public string fileName { get; set; }
        private bool siGuardo { get; set; }

        private IDataService firebaseService;
        private IStorageService storageService;

                                                    //puede definirse a este nivel o en el constructor
        public string[] TiposTareas { get; set; } = (string[])Enum.GetNames(typeof(eTipoTarea));
        public string[] Prioridad { get; set; } = (string[])Enum.GetNames(typeof(ePrioridad));
        public string[] Estado { get; set; } = (string[])Enum.GetNames(typeof(eEstado));

        public RegistroTareaViewModel(IDataService service, IStorageService storageService)
        {
            tarea = new Tarea();
            firebaseService = service;
            this.storageService = storageService;
            TituloPage = "Nueva Tarea";
            isActivo = false;
            isConfigurable = true;
            isComplete = false;
            siGuardo = false;
            isSelectOtherFile = true;
            archivoSeleccionado = string.Empty;
        }

        [RelayCommand]
        private async Task Guardar()
        {
            if (Tarea.Titulo == null || Tarea.Descripcion == null ||
                string.Empty.Equals(Tarea.Titulo) || string.Empty.Equals(Tarea.Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El titulo y la descripción de tarea no deben estar vacios.", "Ok");
            } else
            {
                // valida los casos de los tipos de tarea
                switch (Tarea.TipoTarea)
                {
                    case eTipoTarea.Normal:
                        await BorraTipoArchivo();
                        Tarea.Encuesta = new Encuesta();
                        break;
                    case eTipoTarea.Encuesta:
                        await BorraTipoArchivo();
                        // si es una tarea tipo encuesta pero no tiene perguntas
                        if (Tarea.Encuesta.Preguntas.Count == 0)
                        {
                            // cambia el tipo a normal por no tener preguntas
                            Tarea.TipoTarea = eTipoTarea.Normal;
                            break;
                        }
                        break;
                    case eTipoTarea.Archivo:
                        Tarea.Encuesta = new Encuesta();
                        break;
                    default:
                        // caso default
                        break;
                }

                // valida si esta editando o agregando uno nuevo
                if (isEditar)
                {
                    await firebaseService.EditTaskAsync(Tarea);
                }
                else
                {
                    Tarea.Estado = eEstado.Activo;
                    await firebaseService.AddTask(Tarea);
                }
                siGuardo = true;
                await Shell.Current.GoToAsync("..");

            }
            
        }

        private async Task BorraTipoArchivo()
        {
            if (!string.IsNullOrEmpty(Tarea.NombreArchivo))
            {
                await storageService.DeleteFile(Tarea.NombreArchivo);
            }
            Tarea.URL = string.Empty;
            Tarea.NombreArchivo = string.Empty;
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
                    IsComplete = true;
                }
            }
            if (query.TryGetValue("ENCUESTA", out value))
            {
                Tarea.Encuesta = (Encuesta)value;
            }
        }

        [RelayCommand]
        public void AbrirRegistroEncuesta()
        {
            Dictionary<string, object> parametros = new()
            {
                ["ENCUESTA_EXIST"] = Tarea.Encuesta,
            };
            Shell.Current.GoToAsync(nameof(RegistroEncuestaPage), parametros);
        }
        
        [RelayCommand]
        public async Task Cancelar()
        {
            Tarea.Estado = eEstado.Cancelado;
            await firebaseService.EditTaskAsync(Tarea);
            _ = Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task SeleccionarArchivo()
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

        [RelayCommand]
        public void EditarPregunta(Pregunta pregunta)
        {
            Dictionary<string, object> parametros = new()
            {
                ["ENCUESTA_EXIST"] = Tarea.Encuesta,
                ["PREGUNTA"] = pregunta,
            };
            Shell.Current.GoToAsync(nameof(RegistroEncuestaPage), parametros);
        }

        [RelayCommand]
        public void EliminarPregunta(Pregunta pregunta)
        {
            int index = Tarea.Encuesta.Preguntas.IndexOf(pregunta);
            // Verificar que el índice esté dentro del rango válido
            if (index >= 0 && index < Tarea.Encuesta.Preguntas.Count)
            {
                // Eliminar la pregunta en el índice especificado
                Tarea.Encuesta.Preguntas.RemoveAt(index);
            }
        }

    }
}
