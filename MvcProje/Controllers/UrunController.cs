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
    public class UrunController : Controller
    {

        private readonly AppDbContext _context;
  
        public UrunController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Ürünleri çekerken bağlı olduğu Kategoriyi de dahil ediyoruz(Include)
            var urunler = _context.Urunler.Include(u => u.Kategori).ToList();
            return View (urunler);
        }

        public IActionResult Ekle()
        {
            //!   Veritabanındaki kategorileri çekip dropdown için viewbag içine atıyoruz
            //!   "Id" arka planda kaydedilecek değer, "Ad" ise kullanıcıya gösterilecek metindir.


            ViewBag.KategoriListesi=new SelectList(_context.Kategoriler,"Id","Ad");
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Urun urun)
        {
            //Model kurallarına uyuyorsa veri tabanına ekle
            if(ModelState.IsValid)
            {
                _context.Urunler.Add(urun);
                _context.SaveChanges();//Değişiklikleri Kaydet
                return RedirectToAction("Index");//Kayıt sonrası liste sayfasına yönlendir
            }
            //eğer bir hata varsa formu hatalarla birlikte tekrar göster
            ViewBag.KategoriListesi=new SelectList(_context.Kategoriler,"Id","Ad");
            return View(urun);
        }
    }
}