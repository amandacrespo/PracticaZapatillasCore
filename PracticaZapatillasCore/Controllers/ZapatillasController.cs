using Microsoft.AspNetCore.Mvc;
using PracticaZapatillasCore.Models;
using PracticaZapatillasCore.Repositories;

namespace PracticaZapatillasCore.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int idprod)
        {
            Zapatilla zapa = await this.repo.FindZapatillaAsync(idprod);
            return View(zapa);
        }

        public async Task<IActionResult> Imagenes(int idprod, int pos = 1)
        {
            ImagenDTO datos = await this.repo.GetPaginacionImagenZapatillaAsync(idprod, pos);
            ViewBag.PosicionActual = pos;
            return View(datos);
        }

        public async Task<IActionResult> _ImagenesPartial(int idprod, int pos = 1)
        {
            ImagenDTO datos = await this.repo.GetPaginacionImagenZapatillaAsync(idprod, pos);
            ViewBag.PosicionActual = pos;
            return PartialView("_PartialImagenes", datos);
        }
    }
}
