using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortoAlegrePlus.Models;

namespace PortoAlegrePlus.Controllers
{
    [Authorize]
    public class ComentarioController : Controller
    {
        private PortoAlegrePlusDBContext db = new PortoAlegrePlusDBContext();

        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Reclamacao);
            return View(comentarios.ToList());
        }

        public ActionResult Create(int? id)
        {
            var modelo = new Comentario() { ComentarioID = (int)id, ReclamacaoID = (int)id };
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComentarioID,ReclamacaoID,UserNameID,Conteudo,DataComentario,StatusUsuario")] Comentario comentario, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    comentario.ImageMimeType = image.ContentType;
                    comentario.ImageFile = new byte[image.ContentLength];
                    image.InputStream.Read(comentario.ImageFile, 0, image.ContentLength);
                }

                comentario.DataComentario = DateTime.Now;
                comentario.UserNameID = User.Identity.Name;
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Details", "Reclamacao", new { ID = comentario.ReclamacaoID });
            }

            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Titulo", comentario.ReclamacaoID);
            return View(comentario);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(int id)
        {
            Comentario come = db.Comentarios.Find(id);
            if (come != null && come.ImageFile != null)
            {
                return File(come.ImageFile, come.ImageMimeType);
            }
            else
            {
                return View();
            }
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
