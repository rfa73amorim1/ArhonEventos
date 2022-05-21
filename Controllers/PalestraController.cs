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

namespace AthonEventos.Controllers
{
    public class PalestraController : Controller
    {
        private AthonEventosContext db = new AthonEventosContext();


        // GET: Palestra
        public async Task<ActionResult> Index(string searchString)
        {
            var palestras = db.Palestras.Include(p => p.Evento).Include(p => p.Palestrante);
            if (!String.IsNullOrEmpty(searchString))
            {
                palestras = palestras.Where(s => s.PalestraName.Contains(searchString)
                || s.PalestraTema.Contains(searchString) || s.PalestraDescription.Contains(searchString)
                || s.Evento.EventoName.Contains(searchString));
            }
            return View(await palestras.ToListAsync());
        }

        // GET: Palestra/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestra palestra = await db.Palestras.FindAsync(id);
            if (palestra == null)
            {
                return HttpNotFound();
            }
            return View(palestra);
        }

        // GET: Palestra/Create
        public ActionResult Create()
        {
            ViewBag.EventoID = new SelectList(db.Eventos, "EventoID", "EventoName");
            ViewBag.PalestranteID = new SelectList(db.Palestrantes.Include(c => c.Usuario), "PalestranteId", "Usuario.FullName");
            return View();
        }

        // POST: Palestra/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PalestraID,PalestraName,PalestraTema,PalestraDescription,PalestraUrl,PalestraDt,PalestranteID,EventoID")] Palestra palestra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Palestras.Add(palestra);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.EventoID = new SelectList(db.Eventos, "EventoID", "EventoName", palestra.EventoID);
            ViewBag.PalestranteID = new SelectList(db.Palestrantes, "PalestranteID", "PalestranteDescrição", palestra.PalestranteID);
            return View(palestra);
        }

        // GET: Palestra/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestra palestra = await db.Palestras.FindAsync(id);
            if (palestra == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventoID = new SelectList(db.Eventos, "EventoID", "EventoName", palestra.EventoID);
            ViewBag.PalestranteID = new SelectList(db.Palestrantes.Include(c => c.Usuario), "PalestranteId", "Usuario.FullName", palestra.PalestranteID);
            return View(palestra);
        }

        // POST: Palestra/Edit/5       
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var palestra = await db.Palestras.FindAsync(id);
            if (TryUpdateModel(palestra, "",
               new string[] { "PalestraName", "PalestraTema", "PalestraDescription", "PalestraUrl", "PalestraDt", "PalestranteID", "EventoID" }))
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
            return View(palestra);
        }

        // GET: Palestra/Delete/5       
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
            Palestra palestra = await db.Palestras.FindAsync(id);
            if (palestra == null)
            {
                return HttpNotFound();
            }
            return View(palestra);
        }

        // POST: Palestra/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Palestra palestra = await db.Palestras.FindAsync(id);
                db.Palestras.Remove(palestra);
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
