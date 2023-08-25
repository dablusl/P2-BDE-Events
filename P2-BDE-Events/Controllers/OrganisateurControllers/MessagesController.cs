using Microsoft.AspNetCore.Mvc;

namespace P2_BDE_Events.Controllers.OrganisateurControllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
