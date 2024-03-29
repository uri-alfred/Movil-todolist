using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Rules;
using TodoList.Rules;

namespace TodoList.Models
{
    public class Login
    {
        public Validatable<string> email { get; set; } = Validator.Build<string>()
            .IsRequired("El correo es requerido").WithRule(new EmailRule().WithMessage("El correo no es valido"));
        public Validatable<string> password { get; set; } = Validator.Build<string>()
            .IsRequired("La contraseña es requerida").WithRule(new PasswordRule());
    }
}
