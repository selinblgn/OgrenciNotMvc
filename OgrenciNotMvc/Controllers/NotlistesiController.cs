using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;

namespace OgrenciNotMvc.Controllers
{
    public class NotlistesiController : Controller
    {

        // GET: Notlistesi
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {

            var notlist = db.TBLNOTLAR.ToList();
            return View(notlist);
        }

        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR tbn)
        {
            db.TBLNOTLAR.Add(tbn);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult NotGetir(int id)
        {
            var notlar = db.TBLNOTLAR.Find(id);
            return View("NotGetir", notlar);
        }

        [HttpPost]
        public ActionResult NotGetir(Class1 model, TBLNOTLAR p, int SINAV1 = 0, int SINAV2 = 0, int SINAV3 = 0, int PROJE = 0)
        {
            if (model.islem == "Hesapla")
            {
                int ort = ((SINAV1 + SINAV2 + SINAV3 + PROJE) / 4);
                ViewBag.ort = ort;

            }
            else if (model.islem == "NotGuncelle")
            {
                var snv = db.TBLNOTLAR.Find(p.NOTID);
                snv.SINAV1 = p.SINAV1;
                snv.SINAV2 = p.SINAV2;
                snv.SINAV2 = p.SINAV3;
                snv.PROJE = p.PROJE;
                snv.ORT = p.ORT;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlistesi");

            }
           


            return View();
        }
    

    }
}