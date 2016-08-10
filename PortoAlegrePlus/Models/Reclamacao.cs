using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortoAlegrePlus.Models
{
    public class Reclamacao
    {
        public int ReclamacaoID { get; set; }

        [Display(Name = "Autor")]
        public string UsuarioID { get; set; }

        [Required]
        [Display(Name = "Título")]
        [StringLength(40, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 5)]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataReclamacao { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        [StringLength(100, MinimumLength = 5)]
        public string Endereco { get; set; }

        [Display(Name = "Imagem")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        public string Status { get; set; }

        [Display(Name = "Resolvida? ")]
        public bool StatusUsuario { get; set; }

        [Display(Name = "Encerrada? ")]
        public bool StatusOficial { get; set; }

        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        public int BairroID { get; set; }
        public virtual Bairro Bairro { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}