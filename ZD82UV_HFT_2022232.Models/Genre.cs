using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ZD82UV_HFT_2022232.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        public string GenreKind { get; set; }

        public int SongId { get; set; }
        public int BandId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Song Song { get; set; }
        
        [NotMapped]

        public virtual Band Band { get; set; }
    }
}
