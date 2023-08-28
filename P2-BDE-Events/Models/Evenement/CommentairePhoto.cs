﻿using System.ComponentModel.DataAnnotations;

namespace P2_BDE_Events.Models.Evenement
{
    public class CommentairePhoto : Commentaire
    {
        public int Id { get; set; }
        [Required]
        public virtual Photo Photo { get; set; }

    }
}
