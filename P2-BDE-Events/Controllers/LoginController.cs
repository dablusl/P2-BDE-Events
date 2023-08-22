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

namespace P2_BDE_Events.Controllers
{
    public class LoginController : Controller
    {

        private OrganisateurService OrganisateurService;
        private AuthentificationService AuthentificationService;
        public LoginController()
        {
            OrganisateurService = new OrganisateurService();
            AuthentificationService = new AuthentificationService();    

        }
        [HttpGet]
        public IActionResult Index()
        {
            CompteViewModel compteViewModel = new CompteViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (compteViewModel.Authentifie)
            {

                compteViewModel.Organisateur = OrganisateurService.ObtenirOrganisateur(HttpContext.User.Identity.Name);

                return View("/Home/Index");
            }
            return View(compteViewModel);
        }

        [HttpPost]
        public IActionResult Index(CompteViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                Compte compte = AuthentificationService.Authentifier(viewModel.Compte.Email, viewModel.Compte.MotDePasse);
                int compteId = AuthentificationService.AuthentifierID(viewModel.Compte.Email, viewModel.Compte.MotDePasse);
                //Organisateur organisateur = (Organisateur)AuthentificationService.Authentifier(viewModel.Organisateur.Email, viewModel.Organisateur.MotDePasse);

                if (compte != null) // bon mot de passe    
                {
                    var userClaims = new List<Claim>()
                    {
                       new Claim(ClaimTypes.Email, compteId.ToString()),
                      // new Claim(ClaimTypes.Role, compte.Role),

                    };
                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("/Home/Index");
                }
                ModelState.AddModelError("Organisateur.Email", "Email et/ou mot de passe incorrect(s)");

            }
            return View(viewModel);
        }

        public IActionResult CreerCompteOrganisateur()

        {
            return View();
        }
        [HttpPost]
        public IActionResult CreerCompteOrganisateur(Organisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                int id = OrganisateurService.AjouterOrganisateur(utilisateur.Email, utilisateur.MotDePasse);


                // methode des cookies à supprimer pour rediriger vers page connexion après création
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
                // jusqu'ici

                return Redirect("/");
            }
            return View(utilisateur);
        }
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
