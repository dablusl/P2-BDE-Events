using Microsoft.EntityFrameworkCore;
using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Comptes;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Prestations
{
    public class PrestationService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public PrestationService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerPrestation(Prestation prestation)
        {
            _bddContext.Prestations.Add(prestation);
            _bddContext.SaveChanges();
            return prestation.Id;
        }
        public void ModifierPrestation(int id, Prestation modifications)
        {
            Prestation cible = _bddContext.Prestations.Find(id);
            if (cible != null)
            {
                cible = modifications;
                _bddContext.SaveChanges();
            }
        }
        public Prestation ObtenirPrestation(int id)
        {
            return _bddContext.Prestations.Find(id);
        }
        public List<Prestation> ObtenirToutesLesPrestations()
        {
            return _bddContext.Prestations.ToList();
        }
        public List<Prestation> ObtenirPrestationsParPrestataire(int idPrestataire)
        {
            return _bddContext.Prestations
                .Where(p => p.PrestataireId == idPrestataire)
                .ToList();
        }
        public int ObtenirIdPrestataire(int IdPrestataire)
        {
            // Recherchez le prestataire dans la base de données en fonction de son nom
            var prestataire = _bddContext.Prestataires.FirstOrDefault(p => p.Id == IdPrestataire);

            // Si le prestataire est trouvé, retournez son ID, sinon, retournez une valeur par défaut, par exemple -1
            return prestataire?.Id ?? -1;
        }

        public void SupprimerPrestation(int id)
        {
            Prestation cible = _bddContext.Prestations.Find(id);
            if (cible != null)
            {
                _bddContext.Prestations.Remove(cible);
                _bddContext.SaveChanges();
            }
        }

        public int CreerPropositionPrestation(PropositionPrestation proposition)
        {
            _bddContext.Propositions.Add(proposition);
            _bddContext.SaveChanges();
            return proposition.Id;
        }

        public void NettoyerPropositions(int idLigne)
        {
            List<PropositionPrestation> propositionsASupprimer = _bddContext
                .Propositions.Where(p => p.LigneEvenementId == idLigne).ToList();

            _bddContext.Propositions.RemoveRange(propositionsASupprimer);
            _bddContext.SaveChanges();
        }
        public List<PropositionPrestation> GetPropositionsDuPrestataire(int idPrestataire)
        {
            return _bddContext.Propositions
                .Include(p => p.Prestation)
                .Where(p => p.Prestation.PrestataireId == idPrestataire)
                .ToList();
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}