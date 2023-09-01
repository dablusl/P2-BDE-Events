using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Evenement.Enums;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2_BDE_Events.Services.Evenements
{
    public class EvenementService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public EvenementService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerEvenement(Evenement evenement, int idOrga, string photoPath)
        {
            evenement.Organisateur = null;
            evenement.OrganisateurId = idOrga;
            evenement.CoverPhotoPath = photoPath;
            evenement.Etat = EtatEvenement.OUVERT;
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
            var evenement = _bddContext.Evenements.Include(e => e.Reservations).FirstOrDefault(e => e.Id == evenementId);

            if (participant == null || evenement == null)
            {
                throw new ArgumentException("Participant ou Evènement non trouvé");
            }

            var reservation = new Reserver
            {
                ParticipantId = participantId,
                EvenementId = evenementId,
                DateReservation = DateTime.Now
            };

            evenement.NbParticipants = evenement.Reservations.Count + 1;

            _bddContext.Reservations.Add(reservation);
            _bddContext.SaveChanges();
        }
        public List<Evenement> ObtenirEvenementsReservesParParticipant(int participantId)
        {
            var participant = _bddContext.Participants.Find(participantId);

            if (participant == null)
            {
                throw new ArgumentException("Participant non trouvé");
            }

            var evenementsReserves = _bddContext.Evenements
                .Where(e => e.Reservations.Any(r => r.ParticipantId == participantId))
                .ToList();

            return evenementsReserves;
        }

        public List<Participant> ObtenirParticipants(int evenementId)
        {
            return _bddContext.Reservations
                .Include(r => r.Participant)
                .ThenInclude(p => p.Compte)
                .Where(r => r.EvenementId == evenementId)
                .Select(r => r.Participant)
                //.AsEnumerable()
                .ToList();
        }
        public List<Evenement> ObtenirEvenementsOrganisateur(int organisateurId)
        {
            return _bddContext.Evenements
                .Where(e => e.OrganisateurId == organisateurId)
                .ToList();
        }
        public List<Evenement> EnAppelDoffre(List<TypeDePrestation> types)
        {
            return _bddContext.Evenements
                .Include(e => e.Lignes)
                    .ThenInclude(l => l.Prestation)
                .Where(e => e.Etat == EtatEvenement.OUVERT &&
                    e.Lignes.Any(l => types.Contains(l.Type)))
                .ToList();
        }
        public List<Evenement> ObtenirEvenementsParUniversite(string universite)
        {

            if (!string.IsNullOrEmpty(universite))
            {
                return _bddContext.Evenements
                    .Include(o => o.Organisateur)
                    .ThenInclude(p => p.Participant)
                    .Where(e => e.Organisateur.Participant.Universite == universite)
                           .ToList();
            }

            return null;
        }

        public void PublierEvenement(int idEvenement)
        {
            Evenement evenement = _bddContext.Evenements
                .Include(e => e.Lignes)
                .ThenInclude(l => l.Prestation)
                .FirstOrDefault(e => e.Id == idEvenement);

            if (evenement.Lignes.Count(l => l.Prestation == null) == 0)
            {
                evenement.Etat = EtatEvenement.PUBLIE;
                _bddContext.SaveChanges();
            }
        }

        public double RecalculerBillet(int idEvenement)
        {
            Evenement evenement = _bddContext.Evenements.Include(e => e.Lignes).FirstOrDefault(e => e.Id == idEvenement);

            evenement.PrixBillet = evenement.Lignes
                .Select(l => l.TarifProposee)
                .Aggregate((sum, tarif) => sum + tarif) / evenement.MaxParticipants;

            _bddContext.SaveChanges();

            return evenement.PrixBillet;
        }

        public List<Evenement> EvenementsPrestataire(int idCompte, EtatEvenement etat)
        {
            int idPrestataire = _bddContext.Prestataires.Where(p => p.CompteId == idCompte).ToList()[0].Id;

            return _bddContext.Evenements
                .Where(e => e.Etat == etat)
                .Include(e => e.Lignes)
                    .ThenInclude(l => l.Prestation)
                    .Where(e => e.Lignes.Any(l => l.Prestation.PrestataireId == idPrestataire))
                    .ToList();

        }



        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
