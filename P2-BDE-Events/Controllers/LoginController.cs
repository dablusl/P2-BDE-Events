﻿using Microsoft.AspNetCore.Authentication;
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
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;

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
            Console.WriteLine("Avant le IsValid");
            if (ModelState.IsValid)
            {
                Console.WriteLine("On lance la récupération du compte");

                Compte compte = AuthentificationService.Authentifier(viewModel.Compte.Email, viewModel.Compte.MotDePasse);


                if (compte != null)    
                {
                    Console.WriteLine("On a récupéré un compte");
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
                   //new Claim(ClaimTypes.NameIdentifier, viewModel.Compte.Id),
                   new Claim(ClaimTypes.Email, viewModel.Compte.Email),
                   new Claim(ClaimTypes.Role, viewModel.Compte.Profil),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
                // jusqu'ici

                HttpContext.Session.SetString("iDCompte", id.ToString());

                if(viewModel.Compte.Profil == "Organisateur")
                    return RedirectToAction("CreaCompteOrga");
                if (viewModel.Compte.Profil == "Prestataire")
                    return View("/Login/CreaComptePresta");
                if (viewModel.Compte.Profil == "Participant")
                    return View("/Login/CreaCompteParticip");
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
       
       public IActionResult CreaCompteOrga()
        {
            Compte compte = CompteService.ObtenirCompte(HttpContext.Session.GetString("iDCompte"));
            CreaCompteOrgaViewModel viewModel = new CreaCompteOrgaViewModel
            {
                Compte = compte,
                Organisateur = new Organisateur
                {
                    Compte = compte
                }
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult CreaCompteOrga(CreaCompteOrgaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CompteService.ModifierCompte(int.Parse(HttpContext.Session.GetString("iDCompte")), viewModel.Compte);
                viewModel.Organisateur.CompteId = int.Parse(HttpContext.Session.GetString("iDCompte"));
                OrganisateurService.CreerOrganisateur(viewModel.Organisateur);
                
                return Redirect("/");
            }
            return View(viewModel);
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
