using AthonEventos.DAL;
using AthonEventos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AthonEventos.Controllers
{
    public class ExibicaoController : Controller
    {
        private AthonEventosContext db = new AthonEventosContext();
        public ActionResult ChamarEvento()
        {
            return View(db.Eventos.ToList());
        }

        public ActionResult ChamarPalestra(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }


        //// GET: Exibicao
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Exibicao/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Exibicao/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Exibicao/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Exibicao/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Exibicao/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Exibicao/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Exibicao/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
