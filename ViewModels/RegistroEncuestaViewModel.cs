using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models.Encuestas;

namespace TodoList.ViewModels
{
    public partial class RegistroEncuestaViewModel : ObservableObject, IQueryAttributable
    {

        [ObservableProperty]
        private Pregunta pregunta;

        [ObservableProperty]
        private string tituloPage;

        [ObservableProperty]
        private string nuevaOpcion;

        [ObservableProperty]
        private bool isNuevaOpcion;
        private Encuesta EncuestaReg { get; set; }
        private int indexPregunta {  get; set; }

        public string[] Tipo { get; set; } = (string[])Enum.GetNames(typeof(eTipoPregunta));

        public RegistroEncuestaViewModel()
        {
            EncuestaReg = new Encuesta();
            Pregunta = new Pregunta();
            TituloPage = "Nueva pregunta";
            nuevaOpcion = string.Empty;
            IsNuevaOpcion = false;
            indexPregunta = -1;
        }

        [RelayCommand]
        public async Task GuardarPregunta()
        {
            if (Pregunta.Descripcion == null || string.Empty.Equals(Pregunta.Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La descripción de pregunta no deben estar vacia.", "Ok");
            }
            else
            {
                if (!Pregunta.TipoPregunta.Equals(eTipoPregunta.OpcionMultiple) && !Pregunta.TipoPregunta.Equals(eTipoPregunta.OpcionUnica))
                {
                    Pregunta.Opciones = new();
                }
                // Valida si va a guardar o editar la pregunta
                // -1 indica que es pregunta nueva
                if (indexPregunta == -1)
                {
                    EncuestaReg.Preguntas.Add(Pregunta);
                } 
                else
                {
                    EncuestaReg.Preguntas[indexPregunta] = Pregunta;
                }
                Dictionary<string, object> parametros = new()
                {
                    ["ENCUESTA"] = EncuestaReg,
                };

                _ = Shell.Current.GoToAsync("..", parametros);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            object value = null;
            if (query.TryGetValue("ENCUESTA_EXIST", out value))
            {
                Encuesta encuestaExistente = (Encuesta)value;
                if (encuestaExistente != null)
                {
                    EncuestaReg = encuestaExistente;
                }
            }
            if (query.TryGetValue("PREGUNTA", out value))
            {
                if ((Pregunta)value != null)
                {
                    Pregunta = (Pregunta)value;
                    indexPregunta = EncuestaReg.Preguntas.IndexOf(Pregunta);
                    TituloPage = "Editar pregunta";
                }
            }
        }

        [RelayCommand]
        private void OtraOpcion()
        {
            IsNuevaOpcion = true;
            NuevaOpcion = string.Empty;
        }
        
        [RelayCommand]
        private async Task AgregarOpcion()
        {
            if (string.IsNullOrEmpty(NuevaOpcion))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La descripción de opción no deben estar vacia.", "Ok");
            } else
            {
                Pregunta.Opciones.Add(NuevaOpcion);
                IsNuevaOpcion = false;
            }
        }

        [RelayCommand]
        private void EliminarOpcion(string opcion)
        {
            Pregunta.Opciones.Remove(opcion);
        }

    }
}
