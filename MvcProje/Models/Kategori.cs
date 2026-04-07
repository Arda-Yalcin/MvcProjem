using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProje.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string  Ad { get; set; }
        public List<Urun> Urunler {get; set;}
    }
}