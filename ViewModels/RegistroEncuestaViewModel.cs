using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoList.Models.Encuestas;

namespace TodoList.ViewModels
{
    public partial class RegistroEncuestaViewModel : ObservableObject
    {
        public RegistroEncuestaViewModel() { }

        [RelayCommand]
        public void RegistraEncuesta()
        {
            var pregunta = new Pregunta
            {
                Id = 1,
                Descripcion = "Mi primer pregunta",
                TipoPregunta = eTipoPregunta.Abierta
            };
            var fakeEncuesta = new Encuesta
            {
                Id = 1,
                Preguntas = [pregunta]
            };
            Dictionary<string, object> parametros = new()
            {
                ["ENCUESTA"] = fakeEncuesta
            };
            Shell.Current.GoToAsync("..", parametros);
        }
    }
}
