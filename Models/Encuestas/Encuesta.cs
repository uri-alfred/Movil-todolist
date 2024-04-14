using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TodoList.Models.Encuestas
{
    public class Encuesta
    {
        public ObservableCollection<Pregunta> Preguntas { get; set; }

        public Encuesta()
        {
            Preguntas = new ObservableCollection<Pregunta>();
        }
    }
}
