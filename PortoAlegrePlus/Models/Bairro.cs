using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortoAlegrePlus.Models
{
    public class Bairro
    {
        public int BairroID { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Nome { get; set; }

        public virtual ICollection<Reclamacao> Reclamacoes { get; set; }
    }
}