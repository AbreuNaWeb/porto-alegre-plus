using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortoAlegrePlus.Models;
using Microsoft.AspNet.Identity;

namespace PortoAlegrePlus.Controllers
{
    public class ReclamacaoController : Controller
    {
        private PortoAlegrePlusDBContext db = new PortoAlegrePlusDBContext();

        public ActionResult Index(int? categoriaSelecionada, int? bairroSelecionado, String DataInicio, String DataFim, String statusSelecionado)
        {
            var reclamacoes = db.Reclamacoes.Include(r => r.Bairro).Include(r => r.Categoria);
            String usuario = "visitante";

            foreach (var item in reclamacoes)

                if (item.StatusOficial)
                {
                    item.Status = "Encerrada";
                }
                else if (item.StatusUsuario)
                {
                    item.Status = "Resolvida";
                }
                else
                {
                    item.Status = "Aberta";
                }

            db.SaveChanges();

            reclamacoes = FiltrarReclamacoes(usuario, categoriaSelecionada, bairroSelecionado, DataInicio, DataFim, statusSelecionado);

            return View(reclamacoes.OrderByDescending(a => a.DataReclamacao).ToList());
        }

        [Authorize]
        public ActionResult MinhasReclamacoes(int? categoriaSelecionada, int? bairroSelecionado, String DataInicio, String DataFim, String statusSelecionado)
        {

            string usuario = User.Identity.GetUserName();
            var reclamacoes = FiltrarReclamacoes(usuario, categoriaSelecionada, bairroSelecionado, DataInicio, DataFim, statusSelecionado);

            foreach (var item in reclamacoes)

                if (item.StatusOficial)
                {
                    item.Status = "Encerrada";
                }
                else if (item.StatusUsuario)
                {
                    item.Status = "Resolvida";
                }
                else
                {
                    item.Status = "Aberta";
                }

            db.SaveChanges();

            return View(reclamacoes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);

            if (reclamacao == null)
            {
                return HttpNotFound();
            }

            foreach (var item in reclamacao.Comentarios)
            {
                if (item.StatusUsuario)
                {
                    reclamacao.StatusUsuario = true;
                    reclamacao.Status = "Resolvida";
                }
            }

            var reclamacoes = db.Reclamacoes;
            foreach (var item in reclamacoes)
            {
                if (item.StatusOficial)
                {
                    item.Status = "Encerrada";
                }
            }

            db.SaveChanges();
            return View(reclamacao);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BairroID = new SelectList(db.Bairros, "BairroID", "Nome");
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReclamacaoID,UsuarioID,Titulo,Descricao,DataReclamacao,Endereco,Status,StatusUsuario,StatusOficial,CategoriaID,BairroID")] Reclamacao reclamacao, HttpPostedFileBase image)
        {

            if (!(User.IsInRole("Contas Oficiais")))
            {

                if (ModelState.IsValid)
                {

                    if (image != null)
                    {
                        reclamacao.ImageMimeType = image.ContentType;
                        reclamacao.ImageFile = new byte[image.ContentLength];
                        image.InputStream.Read(reclamacao.ImageFile, 0, image.ContentLength);
                    }

                    reclamacao.UsuarioID = User.Identity.Name;
                    db.Reclamacoes.Add(reclamacao);
                    db.SaveChanges();
                    return RedirectToAction("MinhasReclamacoes");
                }

                ViewBag.BairroID = new SelectList(db.Bairros, "BairroID", "Nome", reclamacao.BairroID);
                ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", reclamacao.CategoriaID);
                return View(reclamacao);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string usuario = User.Identity.GetUserName();
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null || !(usuario == reclamacao.UsuarioID))
            {
                return HttpNotFound();
            }
            ViewBag.BairroID = new SelectList(db.Bairros, "BairroID", "Nome", reclamacao.BairroID);
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", reclamacao.CategoriaID);
            return View(reclamacao);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReclamacaoID,UsuarioID,Titulo,Descricao,DataReclamacao,Endereco,Status,StatusUsuario,StatusOficial,CategoriaID,BairroID")] Reclamacao reclamacao, HttpPostedFileBase image)
        {

            if (reclamacao.Status != "Encerrada")
            {

                if (ModelState.IsValid)
                {

                    if (image != null)
                    {
                        reclamacao.ImageMimeType = image.ContentType;
                        reclamacao.ImageFile = new byte[image.ContentLength];
                        image.InputStream.Read(reclamacao.ImageFile, 0, image.ContentLength);
                    }

                    reclamacao.UsuarioID = User.Identity.Name;
                    db.Entry(reclamacao).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("MinhasReclamacoes");

                }

                ViewBag.BairroID = new SelectList(db.Bairros, "BairroID", "Nome", reclamacao.BairroID);
                ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", reclamacao.CategoriaID);
                return View(reclamacao);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            db.Reclamacoes.Remove(reclamacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao != null && reclamacao.ImageFile != null)
            {
                return File(reclamacao.ImageFile, reclamacao.ImageMimeType);
            }
            else
            {
                return new FilePathResult("~/Imagens/nao-disponivel.jpg", "image/jpeg");
            }
        }

        [Authorize]
        public ActionResult CriarComentario(int id)
        {
            return RedirectToAction("Create", "Comentario", new { id });
        }

        [Authorize(Roles = "Contas Oficiais")]
        public ActionResult EncerrarReclamacao(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);
        }

        [Authorize(Roles = "Contas Oficiais")]
        [HttpPost, ActionName("EncerrarReclamacao")]
        [ValidateAntiForgeryToken]
        public ActionResult EncerrarReclamacaoConfirmed(int id)
        {

            if (User.IsInRole("Contas Oficiais"))
            {
                Reclamacao reclamacao = db.Reclamacoes.Find(id);

                reclamacao.StatusOficial = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        private IQueryable<Reclamacao> FiltrarReclamacoes(String usuario, int? categoriaSelecionada, int? bairroSelecionado, string DataInicio, String DataFim, String statusSelecionado)
        {

            var categorias = db.Categorias.OrderBy(g => g.Nome).ToList();
            var bairros = db.Bairros.OrderBy(g => g.Nome).ToList();

            ViewBag.categoriaSelecionada = new SelectList(categorias, "CategoriaID", "Nome", categoriaSelecionada);
            ViewBag.bairroSelecionado = new SelectList(bairros, "BairroID", "Nome", bairroSelecionado);

            int categoriaID = categoriaSelecionada.GetValueOrDefault();
            int bairroID = bairroSelecionado.GetValueOrDefault();

            var reclamacoes = db.Reclamacoes.Where(c => !categoriaSelecionada.HasValue || c.CategoriaID == categoriaID);

            if (bairroSelecionado.HasValue)
            {
                reclamacoes = reclamacoes.Where(c => !bairroSelecionado.HasValue || c.BairroID == bairroID);
            }

            if (!String.IsNullOrEmpty(DataInicio) && !String.IsNullOrEmpty(DataFim))
            {
                DateTime dataInicio = DateTime.Parse((String)DataInicio);
                DateTime dataFim = DateTime.Parse((String)DataFim);

                reclamacoes = reclamacoes.Where(c => c.DataReclamacao >= dataInicio && c.DataReclamacao <= dataFim);
            }

            if (!String.IsNullOrEmpty(statusSelecionado) && !statusSelecionado.Equals("todos"))
            {
                switch (statusSelecionado)
                {
                    case "aberta":
                        reclamacoes = reclamacoes.Where(c => c.Status == "aberta");
                        break;
                    case "resolvida":
                        reclamacoes = reclamacoes.Where(c => c.Status == "resolvida");
                        break;
                    case "encerrada":
                        reclamacoes = reclamacoes.Where(c => c.Status == "encerrada");
                        break;
                }
            }

            if (!usuario.Equals("visitante"))
            {
                reclamacoes = reclamacoes.Where(x => x.UsuarioID == usuario);
            }

            return reclamacoes;
        }

        public ActionResult MenuCategorias(string categoria = "Acessibilidade")
        {
            var categoriaModel = db.Categorias.Include("Reclamacoes").Single(c => c.Nome == categoria);
            return View(categoriaModel);
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
