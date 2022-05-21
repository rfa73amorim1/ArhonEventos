using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthonEventos.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string UsuarioName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Sobrenome")]
        public string UsuarioSobrenome { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "E-mail")]
        public string UsuarioEmail { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Nascimento")]
        public DateTime UsuarioDt { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10)]
        [Display(Name = "Senha")]
        public string UsuarioPassword { get; set; }

        [Display(Name = "É Aluno Athon?")]
        public bool UsuarioEhAluno { get; set; }



        [Display(Name = "Nome Completo")]
        public string FullName
        {
            get
            {
                return UsuarioName + " " + UsuarioSobrenome;
            }
        }
    }
}