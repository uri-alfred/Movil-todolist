using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models.Encuestas
{
    public class Pregunta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public eTipoPregunta TipoPregunta { get; set; }

        public ObservableCollection<String> Opciones { get; set;}
        public bool IsResuelta { get; set; }

        public ObservableCollection<string> Respuestas { get; set; }

        public Pregunta()
        {
            Opciones = new ObservableCollection<string>();
            Respuestas = new ObservableCollection<string>();
        }
    }
}
