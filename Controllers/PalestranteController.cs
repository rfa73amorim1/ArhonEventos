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
    public class PalestranteController : Controller
    {
        private AthonEventosContext db = new AthonEventosContext();

        // GET: Palestrante
        public async Task<ActionResult> Index(string searchString)
        {
            var palestrantes = db.Palestrantes.Include(p => p.Usuario);
            if (!String.IsNullOrEmpty(searchString))
            {
                palestrantes = palestrantes.Where(s => s.PalestranteDescrição.Contains(searchString)
                || s.Usuario.UsuarioSobrenome.Contains(searchString) || s.Usuario.UsuarioName.Contains(searchString));
            }
            return View(await palestrantes.ToListAsync());
        }

        // GET: Palestrante/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestrante palestrante = await db.Palestrantes.FindAsync(id);
            if (palestrante == null)
            {
                return HttpNotFound();
            }
            return View(palestrante);
        }

        // GET: Palestrante/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "Fullname");
            return View();
        }

        // POST: Palestrante/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PalestranteID,PalestranteDescrição,UsuarioID")] Palestrante palestrante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Palestrantes.Add(palestrante);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioName", palestrante.UsuarioID);
            return View(palestrante);
        }

        // GET: Palestrante/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palestrante palestrante = await db.Palestrantes.FindAsync(id);
            if (palestrante == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "FullName", palestrante.UsuarioID);
            return View(palestrante);
        }

        // POST: Palestrante/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var palestrante = await db.Palestrantes.FindAsync(id);
            if (TryUpdateModel(palestrante, "",
               new string[] { "PalestranteDescrição", "UsuarioID" }))
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
            return View(palestrante);
        }

        // GET: Palestrante/Delete/5        
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
            Palestrante palestrante = await db.Palestrantes.FindAsync(id);
            if (palestrante == null)
            {
                return HttpNotFound();
            }
            return View(palestrante);
        }

        // POST: Palestrante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Palestrante palestrante = await db.Palestrantes.FindAsync(id);
                db.Palestrantes.Remove(palestrante);
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
