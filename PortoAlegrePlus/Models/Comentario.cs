using System;
using System.ComponentModel.DataAnnotations;

namespace PortoAlegrePlus.Models
{
    public class Comentario
    {
        public int ComentarioID { get; set; }
        public int ReclamacaoID { get; set; }
        public string UserNameID { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentário")]
        [StringLength(100, MinimumLength = 3)]
        public string Conteudo { get; set; }

        public DateTime DataComentario { get; set; }

        [Display(Name = "Imagem")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

        [Display(Name = "Resolvida? ")]
        public bool StatusUsuario { get; set; }

        public virtual Reclamacao Reclamacao { get; set; }

    }
}