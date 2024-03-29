using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.ValidationRules.Rules;
using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using TodoList.Rules;

namespace TodoList.Models
{
    public class Registro
    {
        public Validatable<string> Username { get; set; }
        public Validatable<string> Email { get; set; }
        public Validatable<string> Password { get; set; }
        public Validatable<string> RepeatPassword { get; set; }

        public Registro() {
            Username = Validator.Build<string>()
                                .IsRequired("El nombre es requerido.");
            Email = Validator.Build<string>()
                                .IsRequired("El correo es requerido.")
                                .WithRule(new EmailRule().WithMessage("El correo no es valido."));
            Password = Validator.Build<string>()
                                .IsRequired("La contraseña es requerida.")
                                .WithRule(new PasswordRule());
            RepeatPassword = Validator.Build<string>()
                                .When(_ => !string.IsNullOrEmpty(Password.Value))
                                .Must(x => x == Password.Value, "La contraseña no es la misma.");
        }
    }
}
