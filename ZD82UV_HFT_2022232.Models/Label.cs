using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZD82UV_HFT_2022232.Models
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }


        [Required]
        public string LabelName { get; set;}

        [NotMapped]
        public virtual ICollection<Song> Songs { get; set;}


    }
}
