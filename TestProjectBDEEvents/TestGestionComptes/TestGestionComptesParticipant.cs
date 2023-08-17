using P2_BDE_Events.DataAccessLayer;
using P2_BDE_Events.Models.Compte;
using P2_BDE_Events.Services.Comptes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestProjectBDEEvents.TestGestionComptes
{
    [Collection("Database tests")]
    public class TestGestionComptesParticipant
    {
        [Fact]
        public void Creation_Participant_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (ParticipantService participantService = new ParticipantService())
                {
                    // Execution
                    Participant participant = new Participant { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    participantService.CreerParticipant(participant);

                    // Verification
                    List<Participant> participants = participantService.ObtenirTousLesParticipants();
                    Assert.NotNull(participants);
                    Assert.Single(participants);
                    Assert.Equal(participant.Email, participants[0].Email);

                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Modifier_Participant_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();

                using (ParticipantService participantService = new ParticipantService())
                {
                    // Execution
                    Participant participant = new Participant { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    participantService.CreerParticipant(participant);

                    Participant nouveauParticipant = new Participant{ Email = "orgaAZEAZEA2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    participantService.ModifierParticipant(1, nouveauParticipant);
                    // Verification
                    List<Participant> participants = participantService.ObtenirTousLesParticipants();
                    Assert.NotNull(participants);
                    Assert.Single(participants);
                    Assert.Equal(nouveauParticipant.Email, participants[0].Email);
                }
                dal.DeleteCreateDatabase();
            }
        }
        [Fact]
        public void Supprimer_Participant_Verification()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                using (ParticipantService participantService = new ParticipantService())
                {
                    // Execution
                    Participant participant = new Participant { Email = "orga2@orga.com", Prenom = "Jean", Nom = "Pedro", NumeroTelephone = "0102030102" };
                    participantService.CreerParticipant(participant);
                    participantService.SupprimerParticipant(1);

                    // Verification
                    List<Participant> participants = participantService.ObtenirTousLesParticipants();
                    Assert.NotNull(participants);
                    Assert.Empty(participants);
                }
                dal.DeleteCreateDatabase();
            }
        }
    }
}
