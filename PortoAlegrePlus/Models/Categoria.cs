using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortoAlegrePlus.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public virtual ICollection<Reclamacao> Reclamacoes { get; set; }
    }
}