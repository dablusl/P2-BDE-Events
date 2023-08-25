namespace P2_BDE_Events.Models.Compte
{
    public class Compte
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NumeroTelephone { get; set; }
        public string Profil { get;  set; }
        public string PhotoProfilePath { get; set; }
        public string PhotoCoverPath { get; set; }
    }
}
