using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthonEventos.Models
{
    public class Evento
    {
        public int EventoID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Evento")]
        public string EventoName { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "Descrição")]
        public string EventoDescricao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inicio")]
        public DateTime EventoDtInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fim")]
        public DateTime EventoDtFim { get; set; }

        public virtual ICollection<Palestra> Palestras { get; set; }
    }
}