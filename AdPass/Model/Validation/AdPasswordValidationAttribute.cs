using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AdTools;

namespace AdPass.Model.Validation
{
	public class AdPasswordValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!(value is string password)) return InvalidPassword();

			IAdConnector adConnector = AdConnectorFactory.Instance.Build();

			return adConnector.CanAuthenticate("taabaga1", password) ? ValidationResult.Success : InvalidPassword();

		}

		ValidationResult InvalidPassword() => new ValidationResult("Mot de passe invalid");
	}
}
