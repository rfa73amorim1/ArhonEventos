using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthonEventos.Models
{
    public class Palestra
    {
        public int PalestraID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Palestra")]
        public string PalestraName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tema")]
        public string PalestraTema { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Descrição")]
        public string PalestraDescription { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Url")]
        public string PalestraUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime PalestraDt { get; set; }

        [Display(Name = "Palestrante")]
        public int PalestranteID { get; set; }

        [Display(Name = "Evento")]
        public int EventoID { get; set; }

        public virtual Palestrante Palestrante { get; set; }
        public virtual Evento Evento { get; set; }
    }
}