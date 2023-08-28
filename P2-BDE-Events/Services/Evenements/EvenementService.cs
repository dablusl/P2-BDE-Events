using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class EvenementService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public EvenementService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerEvenement(Evenement evenement)
        {
            _bddContext.Evenements.Add(evenement);
            _bddContext.SaveChanges();
            return evenement.Id;
        }

        public void ModifierEvenement(int id, Evenement modifications)
        {
            Evenement cible = _bddContext.Evenements.Find(id);
            if (cible != null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }

        public Evenement ObtenirEvenement(int id)
        {
            return _bddContext.Evenements.Find(id);
        }

        public List<Evenement> ObtenirTousLesEvenements()
        {
            return _bddContext.Evenements.ToList();
        }

        public void SupprimerEvenement(int id)
        {
            Evenement cible = _bddContext.Evenements.Find(id);
            if (cible != null)
            {
                _bddContext.Evenements.Remove(cible);
                _bddContext.SaveChanges();
            }
        }
        public void ReserverEvenement(int participantId, int evenementId)
        {
            var participant = _bddContext.Participants.Find(participantId);
            var evenement = _bddContext.Evenements.Find(evenementId);

            if (participant == null || evenement == null)
            {
                throw new ArgumentException("Participant ou Evènement non trouvé");
            }

            // Créer une nouvelle réservation
            var reservation = new Reserver
            {
                ParticipantId = participantId,
                EvenementId = evenementId,
                DateReservation = DateTime.Now
            };

            // Ajouter la réservation à la base de données
            _bddContext.Reservations.Add(reservation);
            _bddContext.SaveChanges();
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
