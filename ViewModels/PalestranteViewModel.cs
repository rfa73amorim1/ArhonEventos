using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthonEventos.ViewModels
{
    public class PalestranteViewModel
    {
        [Display(Name = "Palestrante")]
        public int PalestranteID { get; set; }

        [StringLength(450)]
        [Display(Name = "Perfil")]
        public string PalestranteDescrição { get; set; }      
        public int UsuarioID { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}