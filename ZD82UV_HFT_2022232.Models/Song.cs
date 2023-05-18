using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZD82UV_HFT_2022232.Models
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SongId { get; set; }

        [Required]
        [StringLength(240)]
        public string SongTitle { get; set;}

        public DateTime ReleaseDate { get; set; }

        public string Album { get; set; }

        public int LabelId { get; set; }

        public double Income { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        [NotMapped]
        public virtual Label Label { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Genre> Genres { get; set; }
        
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Band> Bands { get; set; }
    }
}
