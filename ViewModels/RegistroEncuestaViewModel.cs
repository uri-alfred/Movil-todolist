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

        private ObservableCollection<string> Opciones {  get; set; }

        [ObservableProperty]
        private string nuevaOpcion;

        [ObservableProperty]
        private bool isNuevaOpcion;
        private Encuesta EncuestaReg { get; set; }

        public string[] Tipo { get; set; } = (string[])Enum.GetNames(typeof(eTipoPregunta));

        public RegistroEncuestaViewModel()
        {
            EncuestaReg = new Encuesta();
            Pregunta = new Pregunta();
            TituloPage = "Nueva pregunta";
            Opciones = new ();
            nuevaOpcion = string.Empty;
            IsNuevaOpcion = false;
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
                Pregunta.Opciones = new List<string>(Opciones);
                EncuestaReg.Preguntas.Add(Pregunta);
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
                    TituloPage = "Editar pregunta";
                    if (Pregunta.Opciones.Count > 0)
                    {
                        Opciones.Clear();
                        foreach (var option in Pregunta.Opciones)
                        {
                            Opciones.Add(option);
                        }
                    }
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
        private void AgregarOpcion()
        {
            Opciones.Add(NuevaOpcion);
            IsNuevaOpcion = false;
        }

        [RelayCommand]
        private void EliminarOpcion(int index)
        {
            //Opciones.Remove(string);
            Opciones.RemoveAt(index);
        }
    }
}
