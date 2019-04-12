using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdPass.Model
{
	public class ChangePasswordModel : IValidatableObject
	{

		[Required]
		public string Username { get; set; }

		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string NewPassword1 { get; set; }

		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword1), ErrorMessage = "Les nouveaux mots de passe doivent être identiques.")]
		public string NewPassword2 { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			yield return ValidationResult.Success;
		}
	}
}
