using System;
using System.Collections.Generic;
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

        public List<string> Opciones { get; set;}
        public bool IsResuelta { get; set; }
    }
}
