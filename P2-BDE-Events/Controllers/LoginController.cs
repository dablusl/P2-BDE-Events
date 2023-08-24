using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using P2_BDE_Events.Models;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace P2_BDE_Events.Controllers
{
    public class LoginController : Controller
    {

        private OrganisateurService OrganisateurService;
        private CompteService CompteService;
        private AuthentificationService AuthentificationService;
        public LoginController()
        {
            OrganisateurService = new OrganisateurService();
            CompteService = new CompteService();
            AuthentificationService = new AuthentificationService();

        }
        [HttpGet]
        public IActionResult Index()
        {
            CompteViewModel compteViewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (compteViewModel.Authentifie)
            {
                compteViewModel.Compte = CompteService.ObtenirCompte(HttpContext.User.Identity.Name);

                return Redirect("/Home/Index");

            }
            return View(compteViewModel);
        }

        [HttpPost]
        public IActionResult Index(CompteViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                Compte compte = AuthentificationService.Authentifier(viewModel.Compte.Email, viewModel.Compte.MotDePasse);


                if (compte != null)    
                {
                    var userClaims = new List<Claim>()
                    {
                       new Claim(ClaimTypes.Email, compte.Email),
                       new Claim(ClaimTypes.Role, compte.Profil),

                    };
                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("/");


                }
                ModelState.AddModelError("Compte.Email", "Email et/ou mot de passe incorrect(s)");

            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult CreerCompte()
        {
            var viewModel = new CreationCompteViewModel
            {
                AvailableProfiles = new List<SelectListItem>
        {
                    new SelectListItem { Value = "Prestataire", Text = "Prestataire" },
                    new SelectListItem { Value = "Organisateur", Text = "BDE" },
                    new SelectListItem { Value = "Participant", Text = "Etudiant" }
        }
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult CreerCompte(CreationCompteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Compte.Profil = viewModel.SelectedProfile;
                int id = CompteService.AjouterCompte(viewModel.Compte);
                        


                // methode des cookies à supprimer pour rediriger vers page connexion après création
                var userClaims = new List<Claim>()
                {
                   new Claim(ClaimTypes.Email, viewModel.Compte.Email),
                   new Claim(ClaimTypes.Role, viewModel.Compte.Profil),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
                // jusqu'ici
                return Redirect("/");
            }

            {
                viewModel.AvailableProfiles = new List<SelectListItem>
                    {
                    new SelectListItem { Value = "Prestataire", Text = "Prestataire" },
                    new SelectListItem { Value = "Organisateur", Text = "BDE" },
                    new SelectListItem { Value = "Participant", Text = "Etudiant" }

                };

                return View(viewModel);
            }


        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
