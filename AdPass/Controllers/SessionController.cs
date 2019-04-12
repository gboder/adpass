using AdTools;
using Microsoft.AspNetCore.Mvc;

namespace AdPass.Controllers
{
	[Route("{Controller}/{Action}")]
	public class SessionController : Controller
	{

		public IActionResult Logout()
		{
			return Redirect("https://www.antoinegrosjean.ch/");
		}

		public IActionResult Authenticate(string currentPassword)
		{
			var adConnector = AdConnectorFactory.Instance.Build();
			var user = User.Identity.Name;
			return Json(adConnector.CanAuthenticate(user, currentPassword));
		}
	}
}