using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TodoList.Models.Encuestas
{
    public class Encuesta
    {
        public List<Pregunta> Preguntas { get; set; }
        
        public Respuesta[] Respuestas { get; set; }
        public Encuesta()
        {
            Preguntas = [];
            Respuestas = [];
        }
    }
}
