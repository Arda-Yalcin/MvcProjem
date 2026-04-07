using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using Microsoft.Extensions.Logging;
using MvcProje.Data;

namespace MvcProje.Controllers
{
    public class UrunController : Controller
    {

        private readonly AppDbContext _context;
  
        public UrunController(AppDbContext context)
        {
            _context = context;
        }

    }
}