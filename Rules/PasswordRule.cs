using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.ValidationRules.Interfaces;

namespace TodoList.Rules
{
    public class PasswordRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        public bool Check(string value)
        {
            if (value == null)
            {
                ValidationMessage = "La contraseña es requerida.";
                return false;
            }
            //if (!char.IsLetter(value[0]))
            //{
            //    ValidationMessage = "First character must be a letter.";
            //    return false;
            //}
            //if (!char.IsUpper(value[0]))
            //{
            //    ValidationMessage = "First letter must be Capitalize.";
            //    return false;
            //}
            if (value.Length < 8)
            {
                ValidationMessage = "La contraseña debe tener al menos 8 caracteres.";
                return false;
            }
            //if (!value.Any(char.IsDigit))
            //{
            //    ValidationMessage = "Your password must contain numbers.";
            //    return false;
            //}
            //if (!value.Any(char.IsSymbol) && !value.Any(char.IsPunctuation))
            //{
            //    ValidationMessage = "Your password must contain symbols.";
            //    return false;
            //}
            return true; // Yupiii ! We did !!!
        }
    }
}
