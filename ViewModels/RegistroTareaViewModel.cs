﻿using System;
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

        private bool isEditar { get; set; } = false;

        private IDataService fakeService;

                                                    //puede definirse a este nivel o en el constructor
        public string[] TiposTareas { get; set; } = (string[])Enum.GetNames(typeof(eTipoTarea));
        public string[] Prioridad { get; set; } = (string[])Enum.GetNames(typeof(ePrioridad));
        public string[] Estado { get; set; } = (string[])Enum.GetNames(typeof(eEstado));

        public RegistroTareaViewModel(IDataService service)
        {
            tarea = new Tarea();
            fakeService = service;
            TituloPage = "Nueva Tarea";
            isActivo = false;
            isConfigurable = true;
        }

        [RelayCommand]
        private void Guardar()
        {
            if (isEditar)
            {
                fakeService.EditTaskAsync(Tarea);
            }
            else
            {
                Tarea.Estado = eEstado.Activo;
                fakeService.AddTask(Tarea);
            }
            Shell.Current.GoToAsync("..");
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            object value = null;
            if (query.TryGetValue("TAREA", out value))
            {
                Tarea = value as Tarea;
                TituloPage = "Editar tarea";
                isEditar = true;
                if (Tarea.Estado == eEstado.Activo)
                {
                    IsActivo = true;
                }
                if (Tarea.Estado == eEstado.Completado || Tarea.Estado == eEstado.Cancelado)
                {
                    IsConfigurable = false;
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
        public void Cancelar()
        {
            Tarea.Estado = eEstado.Cancelado;
            fakeService.EditTaskAsync(Tarea);
            Shell.Current.GoToAsync("..");
        }

    }
}
