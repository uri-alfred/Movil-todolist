using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models;
using TodoList.Models.Encuestas;
using TodoList.Services;

namespace TodoList.ViewModels
{
    public partial class FormEncuestaViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private Tarea tarea;
        private IDataService fDataService;
        private int preguntaIndex;
        [ObservableProperty]
        public Pregunta currentQuestion;
        [ObservableProperty]
        private bool isUltimaPregunta;
        [ObservableProperty]
        private bool isNext;
        [ObservableProperty]
        private List<string> optSiNO;
        [ObservableProperty]
        private string respuesta;
        [ObservableProperty]
        private DateTime fechaResp;
        [ObservableProperty]
        private double numericResp;
        public ObservableCollection<string> OptMultiResp { get; set; }


        public FormEncuestaViewModel(IDataService service)
        {
            Tarea = new();
            fDataService = service;
            CurrentQuestion = new();
            preguntaIndex = 0;
            isUltimaPregunta = false;
            isNext = false;
            optSiNO = new();
            OptSiNO.Add("Sí");
            OptSiNO.Add("No");
            Respuesta = "";
            FechaResp = DateTime.Now;
            OptMultiResp = new();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            object value = null;
            if (query.TryGetValue("TAREA_COMPLETE", out value))
            {
                Tarea = (Tarea)value;
                CurrentQuestion = Tarea.Encuesta.Preguntas.Count > 0 ? Tarea.Encuesta.Preguntas[preguntaIndex] : new();
                if (Tarea.Encuesta.Preguntas.Count == 1)
                {
                    IsUltimaPregunta = true;
                } else if (Tarea.Encuesta.Preguntas.Count > 1)
                {
                    IsNext = true;
                }
            }
        }

        [RelayCommand]
        public async Task GuardarRespuestas()
        {
            string msgValid = Validate();
            if (string.IsNullOrEmpty(msgValid))
            {
                GuardarResp();
                await fDataService.EditTaskAsync(Tarea);
                _ = Shell.Current.GoToAsync("..");
            } else
            {
                await Application.Current.MainPage.DisplayAlert("Error", msgValid, "Ok");
            }
        }

        [RelayCommand]
        private async Task Next()
        {
            string msgValid = Validate();
            if (string.IsNullOrEmpty(msgValid))
            {
                GuardarResp();
                preguntaIndex = Tarea.Encuesta.Preguntas.IndexOf(CurrentQuestion);
                // valida si la proxima pregunta es la ultima
                // +2 porque aún no se le incrementa 1 por la siguiente posición y 1 más por la posición de la lista vs total
                if ((preguntaIndex + 2) == Tarea.Encuesta.Preguntas.Count)
                {
                    IsUltimaPregunta = true;
                    IsNext = false;

                }
                // valida si el index de pregunta esta dentro del rango del número de preguntas
                if (preguntaIndex < Tarea.Encuesta.Preguntas.Count - 1)
                {
                    preguntaIndex++;
                    CurrentQuestion = Tarea.Encuesta.Preguntas[preguntaIndex];
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", msgValid, "Ok");
            }
            
        }

        private void GuardarResp()
        {
            switch (CurrentQuestion.TipoPregunta)
            {
                case eTipoPregunta.Numerica:
                    CurrentQuestion.Respuestas.Clear();
                    CurrentQuestion.Respuestas.Add(NumericResp.ToString());
                    break;
                case eTipoPregunta.OpcionMultiple:
                    foreach (var optResp in OptMultiResp)
                    {
                        CurrentQuestion.Respuestas.Add(optResp);
                    }
                    break;
                case eTipoPregunta.Fecha:
                    CurrentQuestion.Respuestas.Clear();
                    CurrentQuestion.Respuestas.Add(FechaResp.ToString());
                    break;
                default:
                    // Abierta, SiNo, Opción unica
                    CurrentQuestion.Respuestas.Clear();
                    CurrentQuestion.Respuestas.Add(Respuesta);
                    break;
            }
        }

        private string Validate()
        {
            string res = string.Empty;
            switch (CurrentQuestion.TipoPregunta)
            {
                case eTipoPregunta.Abierta:
                    if (string.IsNullOrEmpty(Respuesta))
                    {
                        res = "El campo de texto debe ser llenado.";
                    }
                    break;
                case eTipoPregunta.Numerica:
                    //
                    break;
                case eTipoPregunta.SiNo:
                    if (string.IsNullOrEmpty(Respuesta))
                    {
                        res = "Debes seleccionar una opción Sí/No.";
                    }
                    break;
                case eTipoPregunta.OpcionMultiple:
                    if (OptMultiResp.Count == 0)
                    {
                        res = "Debes seleccionar al menos una opción.";
                    }
                    break;
                case eTipoPregunta.OpcionUnica:
                    if (string.IsNullOrEmpty(Respuesta))
                    {
                        res = "Debes seleccionar una opción.";
                    }
                    break;
                case eTipoPregunta.Fecha:
                    if (string.IsNullOrEmpty(FechaResp.ToString()))
                    {
                        res = "Debes ingresar una fecha.";
                    }
                    break;
                default:
                    break;
            }

            return res;
        }

        [RelayCommand]
        private void CkeckOption(string option)
        {

            if (OptMultiResp.Contains(option))
            {
                OptMultiResp.Remove(option);
            } else
            {
                OptMultiResp.Add(option);
            }
        }

        [RelayCommand]
        private void Seleccionada(string option)
        {
            Respuesta = option;
        }

    }
}
