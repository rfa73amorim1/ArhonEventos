using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthonEventos.Models
{
    public class Certificado
    {
        public int CertificadoID { get; set; }

        [Required]
        [Display(Name = "Participante")]
        public int UsuarioID { get; set; }

        [Required]
        [Display(Name = "Palestra")]
        public int PalestraID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Emissão")]
        public DateTime CertificadoDt { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Palestra Palestra { get; set; }
    }
}