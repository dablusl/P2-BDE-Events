﻿namespace P2_BDE_Events.Models.Compte
{
    public class Administrateur
    {
        public int Id { get; set; }
        public virtual Compte Compte { get; set; }
    }
}
