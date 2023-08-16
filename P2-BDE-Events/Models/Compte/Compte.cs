namespace P2_BDE_Events.Models.Compte
{
    public abstract class Compte
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NumeroTelephone { get; set; }

        public string Authorisation { get;  set; }



    }
}