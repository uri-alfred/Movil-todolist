using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.ValidationRules.Interfaces;
using TodoList.Models;

namespace TodoList.Rules
{
    public class TipoTareaRule : IValidationRule<eTipoTarea>
    {
        public string ValidationMessage { get; set; }

        public bool Check(eTipoTarea value)
        {
            if (value.ToString() == string.Empty)
            {
                ValidationMessage = "Necesitas seleccionar un tipo de tarea.";
                return false;
            }

            return true;
        }
    }
}
