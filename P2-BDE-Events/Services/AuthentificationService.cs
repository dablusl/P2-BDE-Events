using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P2_BDE_Events.Services
{
    public class AuthentificationService
    {
        private readonly BDDContext _bddContext;

        public AuthentificationService()
        {
            _bddContext = new BDDContext();
        }

        public Compte Authentifier(string email, string motDePasse)
        {
            string passWord = EncodeMD5(motDePasse);
            Administrateur admin = this._bddContext.Administrateurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (admin!=null) { return admin; }
            Organisateur orga = this._bddContext.Organisateurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (orga != null) { return orga; }
            Participant particip = this._bddContext.Participants.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (particip != null) { return particip; }
            Prestataire presta = this._bddContext.Prestataires.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (presta != null) { return presta; }
            
            return null;
        }

        public int AuthentifierID(string email, string motDePasse)
        {
            string passWord = EncodeMD5(motDePasse);
            Administrateur admin = this._bddContext.Administrateurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (admin != null) { return admin.Id; }
            Organisateur orga = this._bddContext.Organisateurs.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (orga != null) { return orga.Id; }
            Participant particip = this._bddContext.Participants.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (particip != null) { return particip.Id; }
            Prestataire presta = this._bddContext.Prestataires.FirstOrDefault(u => u.Email == email && u.MotDePasse == passWord);
            if (presta != null) { return presta.Id; }

            return -1;

        }

        public int AjouterOrganisateur(string email, string password)
        {
            string motDePasse = EncodeMD5(password);
            Organisateur orga = new Organisateur() { Email = email, MotDePasse = motDePasse };
            this._bddContext.Organisateurs.Add(orga);

            this._bddContext.SaveChanges();
            return orga.Id;
        }



        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "BDEEVENTS" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }



    }
}
