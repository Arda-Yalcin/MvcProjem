using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcProje.Data;
using MvcProje.Models;

namespace MvcProje.Controllers
{
    public class KategoriController : Controller
    {
        private readonly AppDbContext _context;
  
        public KategoriController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var kategoriler = _context.Kategoriler.ToList();
            return View (kategoriler);
        }

        public IActionResult Ekle()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Ekle(Kategori kategori)
        {
            //Model kurallarına uyuyorsa veri tabanına ekle
            if(ModelState.IsValid)
            {
                _context.Kategoriler.Add(kategori);
                _context.SaveChanges();//Değişiklikleri Kaydet
                return RedirectToAction("Index");//Kayıt sonrası liste sayfasına yönlendir
            }
            //eğer bir hata varsa formu hatalarla birlikte tekrar göster
            ViewBag.KategoriListesi=new SelectList(_context.Kategoriler,"Id","Ad");
            return View(kategori);
        }
        [HttpGet]
        public IActionResult Sil(int? id)
        {

            if(id==null) return NotFound();
            //Silinecek ürünü ve kategorisini buluyoruz
            var kategori = _context.Kategoriler.FirstOrDefault(m => m.Id==id);

            if(kategori==null) return NotFound();
            return View(kategori);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            bool bagliUrunVarmi= _context.Urunler.Any(u => u.KategoriId == id);
            if (bagliUrunVarmi)
            {
                TempData["HataMEsaji"]="Bu Kategori Silinemez! Çünkü Bu Kategorinin Içerisinde Hala Ürün Bulunmakta";
                return RedirectToAction(nameof(Sil), new{id=id});
            }
            var kategori = _context.Kategoriler.Find(id);

            if(kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); //nameof ile aynı anlama geliyor
        }
    }
}