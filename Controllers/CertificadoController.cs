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
    public class CertificadoController : Controller
    {
        private AthonEventosContext db = new AthonEventosContext();

        // GET: Certificado
        public async Task<ActionResult> Index(string searchString)
        {
            var certificados = db.Certificados.Include(c => c.Palestra).Include(c => c.Usuario);
            if (!String.IsNullOrEmpty(searchString))
            {
                certificados = certificados.Where(s => s.Palestra.PalestraName.Contains(searchString)
                || s.Usuario.UsuarioName.Contains(searchString) || s.Usuario.UsuarioSobrenome.Contains(searchString));
            }
            return View(await certificados.ToListAsync());
        }

        // GET: Certificado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificado certificado = await db.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return HttpNotFound();
            }
            return View(certificado);
        }

        // GET: Certificado/Create
        public ActionResult Create()
        {
            ViewBag.PalestraID = new SelectList(db.Palestras, "PalestraID", "PalestraName");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "FullName");
            return View();
        }

        // POST: Certificado/Create     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CertificadoID,UsuarioID,PalestraID,CertificadoDt")] Certificado certificado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Certificados.Add(certificado);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.PalestraID = new SelectList(db.Palestras, "PalestraID", "PalestraName", certificado.PalestraID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioName", certificado.UsuarioID);
            return View(certificado);
        }

        // GET: Certificado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificado certificado = await db.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return HttpNotFound();
            }
            ViewBag.PalestraID = new SelectList(db.Palestras, "PalestraID", "PalestraName", certificado.PalestraID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "UsuarioID", "FullName", certificado.UsuarioID);
            return View(certificado);
        }

        // POST: Certificado/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var certificado = await db.Certificados.FindAsync(id);
            if (TryUpdateModel(certificado, "",
               new string[] { "UsuarioID", "PalestraID", "CertificadoDt" }))
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
            return View(certificado);
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
            Certificado certificado = await db.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return HttpNotFound();
            }
            return View(certificado);
        }


        // POST: Evento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Certificado certificado = await db.Certificados.FindAsync(id);
                db.Certificados.Remove(certificado);
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
