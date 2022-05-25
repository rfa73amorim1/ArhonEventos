using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AthonEventos.DAL;
using AthonEventos.Models;
using AthonEventos.ViewModels;
using System.IO;

namespace AthonEventos.Controllers
{
    public class EventoController : Controller
    {
        private AthonEventosContext db = new AthonEventosContext();

        // GET: Evento
        public async Task<ActionResult> Index(string searchString)
        {
            var eventos = from s in db.Eventos
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                eventos = eventos.Where(s => s.EventoDescricao.Contains(searchString)
                                        || s.EventoName.Contains(searchString));
            }

            return View(await eventos.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = await db.Eventos.FindAsync(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            var evento = new EventoViewModel();
            return View(evento);
        }

        // POST: Evento/Create     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventoViewModel eventoViewModel)
        {
            try
            {
                var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

                if (eventoViewModel.ImageUpload == null || eventoViewModel.ImageUpload.ContentLength == 0)
                {
                    ModelState.AddModelError("ImageUpload", "Este campo é obrigatório");
                }
                else if (!imageTypes.Contains(eventoViewModel.ImageUpload.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Escolha imagem GIF, JPG ou PNG.");
                }

                if (ModelState.IsValid)
                {
                    var evento = new Evento();
                    evento.EventoName = eventoViewModel.EventoName;
                    evento.EventoDescricao = eventoViewModel.EventoDescricao;
                    evento.EventoDtInicio = eventoViewModel.EventoDtInicio;
                    evento.EventoDtFim = eventoViewModel.EventoDtFim;                    
                    db.Eventos.Add(evento);
                    using (var binaryReader = new BinaryReader(eventoViewModel.ImageUpload.InputStream))
                        evento.Imagem = binaryReader.ReadBytes(eventoViewModel.ImageUpload.ContentLength);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(eventoViewModel);
        }

        // GET: Evento/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = await db.Eventos.FindAsync(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Evento/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var evento = await db.Eventos.FindAsync(id);
            if (TryUpdateModel(evento, "",
               new string[] { "EventoName", "EventoDescricao", "EventoDtInicio", "EventoDtFim" }))
            {
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(evento);
        }


        // GET: Evento/Delete/5        
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Evento evento = await db.Eventos.FindAsync(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Evento evento = await db.Eventos.FindAsync(id);
                db.Eventos.Remove(evento);
                await db.SaveChangesAsync();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
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
