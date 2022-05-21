using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthonEventos.Models
{
    public class Palestrante
    {
        [Display(Name = "Palestrante")]
        public int PalestranteID { get; set; }

        [StringLength(450)]
        [Display(Name = "Perfil")]
        public string PalestranteDescrição { get; set; }
        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}