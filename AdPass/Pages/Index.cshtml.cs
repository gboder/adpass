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
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                try
                {
                    // Process with the change
                    AdConnectorFactory.Instance.Build().ChangePassword(Model.Username, Model.CurrentPassword, Model.NewPassword1);
                    Model.ErrorMessage = default(string);
                }
                catch (Exception e)
                {
                    Model.ErrorMessage = e.Message;
                    return await Task.FromResult(Page());
                    // TODO Log
                }

                // Force user to re-logon
                return await Task.FromResult(RedirectToPage("./Done"));
            }
            else
            {
                // Show error
                Model.ErrorMessage = string.Join(',',
                    ModelState.Values.Where(v => v.ValidationState == ModelValidationState.Invalid)
                    .SelectManyAdde(v => v.Errors.Select(e => e.ErrorMessage)));
                return await Task.FromResult(Page());
            }
        }

    }
}
