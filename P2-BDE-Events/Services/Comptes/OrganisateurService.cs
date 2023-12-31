﻿using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Comptes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace P2_BDE_Events.Services.Comptes
{
    public class OrganisateurService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public OrganisateurService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerOrganisateur(Organisateur organisateur, int IdCompte)
        {
            Compte compte = _bddContext.Comptes.Find(IdCompte);
            organisateur.Participant.Compte = compte;
            _bddContext.Organisateurs.Add(organisateur);
            _bddContext.SaveChanges();
            return organisateur.Id;
        }

        //public void ModifierOrganisateur(int id, Organisateur modifications)
        //{
        //    Organisateur cible = _bddContext.Organisateurs.Find(id);
        //    if (cible != null)
        //    {
        //        cible.Email = modifications.Email;
        //        cible.Prenom = modifications.Prenom;
        //        cible.Nom = modifications.Nom;
        //        cible.NumeroTelephone = modifications.NumeroTelephone;

        //        _bddContext.SaveChanges();
        //    }
        //}

        public Organisateur ObtenirOrganisateur(Participant participant)
        {
            return _bddContext.Organisateurs
                .Include(o => o.Participant)
                .Where( organisateur => organisateur.Participant.Id == participant.Id)
                .ToList()[0];

            //return _bddContext.Organisateurs.Where(organisateur => organisateur.Compte == compte);
        }
        public Organisateur ObtenirOrganisateur(int id)
        {
            return _bddContext.Organisateurs.Find(id);
        }

        //public Organisateur ObtenirOrganisateur(string idStr)
        //{
        //    int id;
        //    if (int.TryParse(idStr, out id))
        //    {
        //        return this.ObtenirOrganisateur(id);
        //    }
        //    return null;
        //}

        //public List<Organisateur> ObtenirTousLesOrganisateurs()
        //{
        //    return _bddContext.Organisateurs.ToList();
        //}

        //public void SupprimerOrganisateur(int id)
        //{
        //    Organisateur cible = _bddContext.Organisateurs.Find(id);
        //    if (cible != null)
        //    {
        //        _bddContext.Organisateurs.Remove(cible);
        //        _bddContext.SaveChanges();
        //    }
        //}


        //public int AjouterOrganisateur(string email, string password)
        //{
        //    string motDePasse = EncodeMD5(password);
        //    Organisateur orga = new Organisateur() { Email = email, MotDePasse = motDePasse };
        //    this._bddContext.Organisateurs.Add(orga);
        //    this._bddContext.SaveChanges();
        //    return orga.Id;
        //}



        //public static string EncodeMD5(string motDePasse)
        //{
        //    string motDePasseSel = "BDEEVENTS" + motDePasse + "ASP.NET MVC";
        //    return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        //}

        public Organisateur GetOrganisateurParCompte(int idCompte)
        {
            Compte compte = new CompteService().ObtenirCompte(idCompte);
            Participant participant = new ParticipantService().ObtenirParticipant(compte);
            return this.ObtenirOrganisateur(participant);
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
