using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Services.Comptes;
using P2_BDE_Events.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using P2_BDE_Events.Services;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using P2_BDE_Events.Models.Comptes;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace P2_BDE_Events.Controllers

{
    public class LoginController : Controller
    {

        private OrganisateurService OrganisateurService;
        private CompteService CompteService;
        private AuthentificationService AuthentificationService;
        private ParticipantService ParticipantService;
        private PrestataireService PrestataireService;
        private AdministrateurService AdministrateurService;
        public LoginController()
        {
            OrganisateurService = new OrganisateurService();
            CompteService = new CompteService();
            AuthentificationService = new AuthentificationService();
            ParticipantService = new ParticipantService();
            PrestataireService = new PrestataireService();
            AdministrateurService = new AdministrateurService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            CompteViewModel compteViewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };

            if (compteViewModel.Authentifie && HttpContext.Session.GetString("iDCompte") != null)
            {
                Compte compte = new CompteService().ObtenirCompte(GetIdCompte());

                if (compte.Profil == "Organisateur")
                {
                    return RedirectToAction("MesEvenementsOrga", "MesEvenements", new { area = "OrganisateurControllers" });
                }
                else if (compte.Profil == "Participant")
                {
                    return RedirectToAction("AgendaEvenement", "Agenda", new { area = "ParticipantControllers" });
                }
                else if (compte.Profil == "Prestataire")
                {
                    return RedirectToAction("ToutesLesPrestations", "ConsultationPrestations", new { area = "PrestataireControllers", IdPrestataire = HttpContext.Session.GetString("iDCompte") });
                }
                else if (compte.Profil == "Administrateur")
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "AdministrateurControllers" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
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
               new Claim(ClaimTypes.Sid, compte.Id.ToString()),
            };
                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);
                    HttpContext.Session.SetString("iDCompte", compte.Id.ToString());

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (compte.Profil == "Organisateur")
                        {
                            return RedirectToAction("MesEvenementsOrga", "MesEvenements", new { area = "OrganisateurControllers" });
                        }
                        else if (compte.Profil == "Participant")
                        {
                            return RedirectToAction("AgendaEvenement", "Agenda", new { area = "ParticipantControllers" });
                        }
                        else if (compte.Profil == "Prestataire")
                        {
                            return RedirectToAction("ToutesLesPrestations", "ConsultationPrestations", new { area = "PrestataireControllers" });
                        }
                        else if (compte.Profil == "Administrateur")
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "AdministrateurControllers" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
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
                Compte = new Compte(), // Initialisez Compte ici
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
                   new Claim(ClaimTypes.Sid, viewModel.Compte.Id.ToString())

                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
                // jusqu'ici

                HttpContext.Session.SetString("iDCompte", id.ToString());

                if (viewModel.Compte.Profil == "Organisateur")
                    return RedirectToAction("CreaCompteOrga");
                if (viewModel.Compte.Profil == "Prestataire")
                    return RedirectToAction("CreaComptePresta");
                if (viewModel.Compte.Profil == "Participant")
                    return RedirectToAction("CreaCompteParticipant");
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
                    Participant = new Participant
                    {
                        Compte = compte
                    }

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
                OrganisateurService.CreerOrganisateur(viewModel.Organisateur, int.Parse(HttpContext.Session.GetString("iDCompte")));

                return RedirectToAction("MesEvenementsOrga", "MesEvenements", new { area = "OrganisateurControllers" });
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult CreaCompteParticipant()
        {
            Compte compte = CompteService.ObtenirCompte(HttpContext.Session.GetString("iDCompte"));
            Participant viewModel = new Participant
            {
                Compte = compte
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult CreaCompteParticipant(Participant viewModel)
        {
            if (ModelState.IsValid)
            {
                CompteService.ModifierCompte(int.Parse(HttpContext.Session.GetString("iDCompte")), viewModel.Compte);
                ParticipantService.CreerParticipant(viewModel, int.Parse(HttpContext.Session.GetString("iDCompte")));

                return RedirectToAction("AgendaEvenement", "Agenda", new { area = "ParticipantControllers" });
            }
            return View(viewModel);
        }

        private List<SelectListItem> GetAvailableServiceTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Restaurateur", Text = "Restaurateur" },
                new SelectListItem { Value = "Photographe", Text = "Photographe" },
                new SelectListItem { Value = "Vidéaste", Text = "Vidéaste" },
                new SelectListItem { Value = "Lieu de réception", Text = "Lieu de réception" },
                new SelectListItem { Value = "Service de sécurité", Text = "Service de sécurité" }
            };
        }
        public IActionResult CreaComptePresta()
        {
            Compte compte = CompteService.ObtenirCompte(HttpContext.Session.GetString("iDCompte"));
            CreaComptePrestaViewModel viewModel = new CreaComptePrestaViewModel
            {
                Compte = compte,
                Prestataire = new Prestataire
                {
                    Compte = compte
                },
                AvailableServiceTypes = GetAvailableServiceTypes()

            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult CreaComptePresta(CreaComptePrestaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CompteService.ModifierCompte(int.Parse(HttpContext.Session.GetString("iDCompte")), viewModel.Compte);
                viewModel.Prestataire.TypeActivite = string.Join(", ", viewModel.SelectedServiceTypes);

                PrestataireService.CreerPrestataire(viewModel.Prestataire, int.Parse(HttpContext.Session.GetString("iDCompte")));

                return RedirectToAction("Index", "CreerUnePrestation", new { area = "PrestataireControllers" });
            }
            viewModel.AvailableServiceTypes = GetAvailableServiceTypes();
            return View(viewModel);
        }

        [Authorize(Roles = "Administrateur")]
        [HttpGet]
        public IActionResult CreaCompteAdmin()
        {
            var viewModel = new CreaCompteAdminViewModel();
            return View(viewModel);
        }

        [Authorize(Roles = "Administrateur")]
        [HttpPost]
        public IActionResult CreaCompteAdmin(CreaCompteAdminViewModel viewModel)

        {
            if (ModelState.IsValid)
            {
                viewModel.Compte.Profil = "Administrateur";
                CompteService.AjouterCompte(viewModel.Compte);
                var administrateur = new Administrateur
                {
                    Compte = viewModel.Compte
                };
                AdministrateurService.CreerAdministrateur(administrateur, viewModel.Compte.Id);

                return RedirectToAction("Index", "Dashboard", new { area = "AdministrateurControllers" });
            }
            return View(viewModel);

        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public int GetIdCompte()
        {
            return int.Parse(HttpContext.Session.GetString("iDCompte"));
        }
    }
}
