using System;
using System.ComponentModel.DataAnnotations;

namespace PortoAlegrePlus.Models
{
    public class Informacao
    {
        [Display(Name = "Nome")]
        public String Nome { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Display(Name = "Total de Reclamações")]
        public int TotalReclamacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double PercentualEncerrada { get; set; }

        [Display(Name = "Média de Comentários")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double MediaComentarios { get; set; }

        [Display(Name = "% Status")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double PercentualStatus { get; set; }

    }
}