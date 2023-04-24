using cb_ejercicio2.Data;
using cb_ejercicio2.Logic;
using cb_ejercicio2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cb_ejercicio2.Controllers
{
    public class HomeController : Controller
    {
        private readonly FilesDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env, FilesDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> IndexArchivos()
        {
            

            return View(await _context.CbtProcesodeanalises.FromSqlRaw("cbt_procesodeanalisis_listado").ToListAsync());
        }

        public async Task<IActionResult> FormarZip(int id, string com)
        {
            var archivos = await _context.CbtProcesodecargas.FromSqlRaw($"cbt_procesodeanalisis_archivoscarga {id}").ToListAsync();
            ZipLogic zip = new ZipLogic(_env, $"ArchivosProcesoAnalisis_{id}.zip");
            zip.CreateZipFromFiles(archivos);
            TempData["Created"] = $"Se han comprimido los archivos de {com}";
            return RedirectToAction("IndexArchivos");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}