using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcProje.Models;

namespace MvcProje.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        public DbSet<Kategori> Kategoriler {get; set;}
        public DbSet<Urun> Urunler {get; set;}
    }
}