using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using AdPass.Model;
using AdPass.Model.Validation;
using AdTools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdPass.Pages
{
	public class IndexModel : PageModel
	{
		[BindProperty] public ChangePasswordModel Model { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			Model = new ChangePasswordModel { Username = User.Identity.Name };
			return await Task.FromResult(Page());
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				// Process with the change
				AdConnectorFactory.Instance.Build().ChangePassword(Model.Username, Model.CurrentPassword, Model.NewPassword1);
				// Force user to re-logon
				return await Task.FromResult(RedirectToPage("./Done"));
			}
			else
			{
				// Show error
				return await Task.FromResult(Page());
			}
		}

	}
}
