using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace P2_BDE_Events.Services
{
    public class CompteService : Dal
    {
        //private BDDContext _bddContext;
        //public CompteService()
        //{
        //    _bddContext = new BDDContext();
        //}
        public List<Organisateur> ObtenirTousLesOrganisateurs()
        {
            return _bddContext.Organisateurs.ToList();
        }

        public int AjouterOrganisateur(string email, string password)
        {
            string motDePasse = EncodeMD5(password);
            Organisateur orga = new Organisateur() { Email = email, MotDePasse = motDePasse };
            this._bddContext.Comptes.Add(orga);
            this._bddContext.SaveChanges();
            return orga.Id;
        }
        


        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "BDEEVENTS" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }



        //public Compte ObtenirCompte(int id)
        //{
        //    return this._bddContext.Comptes.Find(id);
        //}
        //public Compte ObtenirCompte(string idStr)
        //{
        //    int id;
        //    if (int.TryParse(idStr, out id))
        //    {
        //        return this.ObtenirCompte(id);
        //    }
        //    return null;

        //}
    }
}
