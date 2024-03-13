using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models.Encuestas;

namespace TodoList.Models
{
    public class Tarea
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; } = DateTime.Now;
        public DateTime FechaFinal { get; set; } = DateTime.Now.AddDays(1);
        public eTipoTarea TipoTarea { get; set; }
        public ePrioridad Prioridad { get; set; }
        public eEstado Estado { get; set; }
        public Encuesta Encuesta { get; set; }
        public string URL { get; set; }

    }
}
