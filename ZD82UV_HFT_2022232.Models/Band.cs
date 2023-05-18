using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZD82UV_HFT_2022232.Models
{
    public class Band
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BandId { get; set; }

        [Required]
        public string BandName { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Song> Songs { get; set;}

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Genre> Genres { get; set;}
    }
}
