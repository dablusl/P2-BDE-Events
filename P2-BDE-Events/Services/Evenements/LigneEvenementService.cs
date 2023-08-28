using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Evenement;
using P2_BDE_Events.Models.Prestations;
using P2_BDE_Events.Models.Prestations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P2_BDE_Events.Services.Evenements
{
    public class LigneEvenementService : IDisposable
    {
        private readonly BDDContext _bddContext;

        public LigneEvenementService()
        {
            _bddContext = new BDDContext();
        }
        public int CreerLigneEvenement(LigneEvenement ligne)
        {
            _bddContext.LignesEvenement.Add(ligne);
            _bddContext.SaveChanges();
            return ligne.Id;
        }

        public void ModifierLigneEvenement(Evenement evenement, TypeDePrestation type, Prestation nouvellePrestation) 
        {
            LigneEvenement cible = _bddContext.LignesEvenement.Find(evenement);
            if (cible != null)
            {
                cible.Prestation = nouvellePrestation;
                _bddContext.SaveChanges();
            }
        }

        public Evenement ObtenirEvenement(int id)
        {
            return _bddContext.Evenements.Find(id);
        }

        public LigneEvenement ObtenirLigneEvenement(int id)
        {
            return _bddContext.LignesEvenement
                .Find(id);
        }

        public void ModifierLigneEvenement(int id, Evenement evenement)
        {
            LigneEvenement ligne = _bddContext.LignesEvenement.Find(id);

            if (ligne != null)
            {
                ligne.Evenement = evenement;
                _bddContext.SaveChanges();
            }

        }

        public List<LigneEvenement> ObtenirLignesDeLevenement(Evenement evenement)
        {
            return _bddContext.LignesEvenement
                .Where(ligne => ligne.Evenement == evenement)
                .ToList();
        }

        public void SupprimerLigneEvenement(Evenement evenement, TypeDePrestation typePrestation)
        {
            LigneEvenement ligne = _bddContext.LignesEvenement.Find(evenement);
            if (ligne != null)
            {
                _bddContext.LignesEvenement.Remove(ligne);
                _bddContext.SaveChanges();
            }
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
