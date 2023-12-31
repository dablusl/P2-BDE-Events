﻿using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Comptes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Comptes
{
    public class ParticipantService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public ParticipantService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerParticipant(Participant participant, int IdCompte)
        {
            Compte compte = _bddContext.Comptes.Find(IdCompte);
            participant.Compte = compte;
            _bddContext.Participants.Add(participant);
            _bddContext.SaveChanges();
            return participant.Id;
        }

        //public void ModifierParticipant(int id, Participant modifications)
        //{
        //    Participant cible = _bddContext.Participants.Find(id);
        //    if (cible != null)
        //    {
        //        cible.Email = modifications.Email;
        //        cible.Prenom = modifications.Prenom;
        //        cible.Nom = modifications.Nom;
        //        cible.NumeroTelephone = modifications.NumeroTelephone;

        //        _bddContext.SaveChanges();
        //    }
        //}
        public Participant GetParticipantParCompte(int idCompte)
        {
            Compte compte = new CompteService().ObtenirCompte(idCompte);
            return _bddContext.Participants
                .Include(p => p.Reservations)
                .Where(participant => participant.Compte.Id == compte.Id)
                .ToList()[0];
            
        }
        //public Participant ObtenirParticipant(int id)
        //{
        //    return _bddContext.Participants.Find(id);
        //}

        public Participant ObtenirParticipant(Compte compte)
        {
            return _bddContext.Participants.Where(participant => participant.Compte.Id == compte.Id).ToList()[0];
        }


        //public List<Participant> ObtenirTousLesParticipants()
        //{
        //    return _bddContext.Participants.ToList();
        //}

        //public void SupprimerParticipant(int id)
        //{
        //    Participant cible = _bddContext.Participants.Find(id);
        //    if (cible != null)
        //    {
        //        _bddContext.Participants.Remove(cible);
        //        _bddContext.SaveChanges();
        //    }
        //}

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
