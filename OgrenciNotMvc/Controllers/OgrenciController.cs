using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci

        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var ogrenciler = db.TBLOGR.ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public ActionResult YeniOgrenci()
        {

            List<SelectListItem> degerler = (from i in db.TBLKULUP.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGR p3)
        {
            var klp = db.TBLKULUP.Where(m => m.KULUPID == p3.TBLKULUP.KULUPID).FirstOrDefault();
            p3.TBLKULUP = klp;
            db.TBLOGR.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
        public ActionResult Sil(int id)
        {
            var ogrenci = db.TBLOGR.Find(id);
            db.TBLOGR.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

     


        public ActionResult Getir(int id)
           
        {
            var ders = db.TBLOGR.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKULUP.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler; 
            return View("Getir",ders);
        }
    
        public ActionResult Güncelle(TBLOGR p)
        {
            var ogrgun = db.TBLOGR.Find(p.OGRID);
            ogrgun.OGRAD = p.OGRAD;
            ogrgun.OGRSOYAD = p.OGRSOYAD;
            ogrgun.OGRKULUP = p.OGRKULUP;
            ogrgun.OGRCINSIYET = p.OGRCINSIYET;
            ogrgun.OGRFOTO = p.OGRFOTO;
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");

        }
    
    
    }

}