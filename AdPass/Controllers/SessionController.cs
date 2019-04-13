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
	}
}