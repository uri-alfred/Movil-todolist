using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.ValidationRules.Interfaces;
using TodoList.Models;

namespace TodoList.Rules
{
    public class PrioridadRule : IValidationRule<ePrioridad>
    {
        public string ValidationMessage { get; set; }

        public bool Check(ePrioridad value)
        {
            if (value.ToString() == string.Empty)
            {
                ValidationMessage = "Necesitas seleccionar una prioridad.";
                return false;
            }

            return true;
        }
    }
}
