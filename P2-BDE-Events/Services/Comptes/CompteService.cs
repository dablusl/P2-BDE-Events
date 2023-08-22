using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace P2_BDE_Events.Services.Comptes
{
    public class CompteService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public CompteService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerCompte(Compte compte)
        {
            _bddContext.Comptes.Add(compte);
            _bddContext.SaveChanges();
            return compte.Id;
        }

        public void ModifierCompte(int id, Compte modifications)
        {
            Compte cible = _bddContext.Comptes.Find(id);
            if (cible != null)
            {
                cible.Email = modifications.Email;
                cible.Prenom = modifications.Prenom;
                cible.Nom = modifications.Nom;
                cible.NumeroTelephone = modifications.NumeroTelephone;

                _bddContext.SaveChanges();
            }
        }

        public Compte ObtenirCompte(int id)
        {
            return _bddContext.Comptes.Find(id);
        }
        public Compte ObtenirCompte(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirCompte(id);
            }
            return null;
        }

        public List<Compte> ObtenirTousLesComptes()
        {
            return _bddContext.Comptes.ToList();
        }

        public void SupprimerCompte(int id)
        {
            Compte cible = _bddContext.Comptes.Find(id);
            if (cible != null)
            {
                _bddContext.Comptes.Remove(cible);
                _bddContext.SaveChanges();
            }
        }


        public int AjouterCompte(string email, string password)
        {
            string motDePasse = EncodeMD5(password);
            Compte orga = new Compte() { Email = email, MotDePasse = motDePasse };
            this._bddContext.Comptes.Add(orga);
            this._bddContext.SaveChanges();
            return orga.Id;
        }



        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "BDEEVENTS" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
