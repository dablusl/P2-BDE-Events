using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Controllers.ParticipantControllers
{
    public class AgendaController : Controller
    {
        private EvenementService EvenementService;

        public AgendaController()
        {
            EvenementService = new EvenementService();
        }
        //public IActionResult AgendaEvenement()
        //{
        //    return View("Views/Participant/AgendaEvenement.cshtml");
        //}

        [HttpGet]
        public IActionResult AgendaEvenement(int? eventId)
        {
            List<Evenement> evenements = EvenementService.ObtenirTousLesEvenements(); // Obtenez la liste complète des événements

            if (eventId.HasValue)
            {
                evenements = evenements.Where(e => e.Id == eventId.Value).ToList(); // Filtrer les événements par identifiant
            }

            // Créez le modèle pour la vue
            var viewModel = new AgendaEvenementViewModel
            {
                Evenements = evenements
            };

            return View("Views/Participant/AgendaEvenement.cshtml", viewModel);
        }
    }
}
