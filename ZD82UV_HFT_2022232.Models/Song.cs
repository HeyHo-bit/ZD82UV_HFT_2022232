﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ZD82UV_HFT_2022232.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SongId { get; set; }

        [Required]
        public string SongTitle { get; set;}

        public DateTime ReleaseDate { get; set; }

        public string Album { get; set; }

        public int LabelId { get; set; }

        public double Income { get; set; }

        public int Rating { get; set; }

        [NotMapped]
        public virtual Label Label { get; set; }

        [NotMapped]
        public virtual ICollection<Genre> Genres { get; set; }
        
        [NotMapped]
        public virtual ICollection<Band> Bands { get; set; }
    }
}
