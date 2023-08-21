using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Services.Evenements;
using P2_BDE_Events.Models.Evenement.Enums;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.ViewComponents
{
    public class PhotoCarouselViewComponent : ViewComponent
    {  
        public IViewComponentResult Invoke()
        {
            List<Evenement> evenements = new EvenementService()
                .ObtenirTousLesEvenements()
                .Where(evenement => evenement.Etat == EtatEvenement.PUBLIE)
                .OrderByDescending(evenement => evenement.DateEvenement)
                .Take(4)
                .ToList();

            return View(evenements);
        }
    }
}
