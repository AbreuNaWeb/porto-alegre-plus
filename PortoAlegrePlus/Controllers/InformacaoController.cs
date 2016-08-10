using PortoAlegrePlus.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PortoAlegrePlus.Controllers
{

    [Authorize(Roles = "Admin")]
    public class InformacaoController : Controller
    {

        private PortoAlegrePlusDBContext db = new PortoAlegrePlusDBContext();
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult DadosCategoria(string dataInicioCategoria, string dataFimCategoria, string statusCategoria)
        {

            if (!String.IsNullOrEmpty(dataInicioCategoria) && !String.IsNullOrEmpty(dataFimCategoria))
            {
                DateTime dataInicio = DateTime.Parse((string)dataInicioCategoria);
                DateTime dataFim = DateTime.Parse((string)dataFimCategoria);

                if (!String.IsNullOrEmpty(statusCategoria) && !statusCategoria.Equals("filtrar"))
                {
                    var dataFiltrada = from reclamacao in db.Reclamacoes.Where(c => c.DataReclamacao >= dataInicio && c.DataReclamacao <= dataFim)
                                       group reclamacao by reclamacao.Categoria into dateGroup
                                       select new Informacao()
                                       {
                                           Nome = dateGroup.Key.Nome,
                                           Quantidade = dateGroup.Count(),
                                           MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                                           PercentualStatus = ((double)(dateGroup.Where(c => c.Status.Equals(statusCategoria)).Count()) / dateGroup.Count()) * 100,

                                       };
                    return PartialView(dataFiltrada.OrderBy(g => g.Quantidade).ToList());
                }
                else
                {
                    var dataFiltrada = from reclamacao in db.Reclamacoes.Where(c => c.DataReclamacao >= dataInicio && c.DataReclamacao <= dataFim)
                                       group reclamacao by reclamacao.Categoria into dateGroup
                                       select new Informacao()
                                       {
                                           Nome = dateGroup.Key.Nome,
                                           Quantidade = dateGroup.Count(),
                                           MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                                           PercentualStatus = 0
                                       };
                    return PartialView(dataFiltrada.OrderBy(g => g.Quantidade).ToList());
                }
            }

            if (!String.IsNullOrEmpty(statusCategoria) && !statusCategoria.Equals("filtro"))
            {

                var dataFiltrada = from reclamacao in db.Reclamacoes
                                   group reclamacao by reclamacao.Categoria into dateGroup
                                   select new Informacao()
                                   {
                                       Nome = dateGroup.Key.Nome,
                                       Quantidade = dateGroup.Count(),
                                       MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                                       PercentualStatus = ((double)(dateGroup.Where(c => c.Status.Equals(statusCategoria)).Count()) / dateGroup.Count()) * 100,
                                   };
                return PartialView(dataFiltrada);
            }

            var data = from reclamacao in db.Reclamacoes
                       group reclamacao by reclamacao.Categoria into dateGroup
                       select new Informacao()
                       {
                           Nome = dateGroup.Key.Nome,
                           Quantidade = dateGroup.Count(),
                           MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                           PercentualStatus = 0
                       };

            return PartialView(data.ToList());
        }


        public ActionResult DadosBairro(string dataInicioBairro, string dataFimBairro, string statusBairro)
        {


            if (!String.IsNullOrEmpty(dataInicioBairro) && !String.IsNullOrEmpty(dataFimBairro))
            {
                DateTime dataInicio = DateTime.Parse((string)dataInicioBairro);
                DateTime dataFim = DateTime.Parse((string)dataFimBairro);

                if (!String.IsNullOrEmpty(statusBairro) && !statusBairro.Equals("filtrar"))
                {
                    var dataFiltrada = from reclamacao in db.Reclamacoes.Where(c => c.DataReclamacao >= dataInicio && c.DataReclamacao <= dataFim)
                                       group reclamacao by reclamacao.Bairro into dateGroup
                                       select new Informacao()
                                       {
                                           Nome = dateGroup.Key.Nome,
                                           Quantidade = dateGroup.Count(),
                                           MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                                           PercentualStatus = ((double)(dateGroup.Where(c => c.Status.Equals(statusBairro)).Count()) / dateGroup.Count()) * 100,
                                       };
                    return PartialView(dataFiltrada.OrderBy(g => g.Quantidade).ToList());
                }
                else
                {
                    var dataFiltrada = from reclamacao in db.Reclamacoes.Where(c => c.DataReclamacao >= dataInicio && c.DataReclamacao <= dataFim)
                                       group reclamacao by reclamacao.Bairro into dateGroup
                                       select new Informacao()
                                       {
                                           Nome = dateGroup.Key.Nome,
                                           Quantidade = dateGroup.Count(),
                                           MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                                           PercentualStatus = 0
                                       };
                    return PartialView(dataFiltrada.OrderBy(g => g.Quantidade).ToList());
                }
            }

            if (!String.IsNullOrEmpty(statusBairro) && !statusBairro.Equals("filtro"))
            {

                var dataFiltrada = from reclamacao in db.Reclamacoes
                                   group reclamacao by reclamacao.Bairro into dateGroup
                                   select new Informacao()
                                   {
                                       Nome = dateGroup.Key.Nome,
                                       Quantidade = dateGroup.Count(),
                                       MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                                       PercentualStatus = ((double)(dateGroup.Where(c => c.Status.Equals(statusBairro)).Count()) / dateGroup.Count()) * 100,
                                   };
                return PartialView(dataFiltrada);
            }

            var data = from reclamacao in db.Reclamacoes
                       group reclamacao by reclamacao.Bairro into dateGroup
                       select new Informacao()
                       {
                           Nome = dateGroup.Key.Nome,
                           Quantidade = dateGroup.Count(),
                           MediaComentarios = dateGroup.Average(g => g.Comentarios.Count),
                           PercentualStatus = 0
                       };

            return PartialView(data.ToList());
        }

        public ActionResult DadosReclamacao()
        {
            var data = new Informacao()
            {
                TotalReclamacao = db.Reclamacoes.Count(),
                PercentualEncerrada = Math.Round(((double)(db.Reclamacoes.Where(c => c.Status.Equals("Encerrada")).Count()) / db.Reclamacoes.Count()) * 100),

            };

            return PartialView(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}