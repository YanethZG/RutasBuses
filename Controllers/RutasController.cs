using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RutasBuses.Models;
using RutasBuses.Repositories;

namespace RutasBuses.Controllers
{
    public class RutasController : Controller
    {
        private readonly IRutasRepository _rutasRepository;

        public RutasController(IRutasRepository rutasRepository)
        {
            _rutasRepository = rutasRepository;
        }

        public IActionResult Index()
        {
            return View(_rutasRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var rutasBuses = _rutasRepository.GetAll();

            ViewBag.RutasBuses = new SelectList(rutasBuses,
                                       nameof(RutasModel.id_Rutas),
                                       nameof(RutasModel.Nombre_Ruta));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RutasModel rutas)
        {
            try
            {
                _rutasRepository.Add(rutas);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(rutas);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rutas = _rutasRepository.GetRutasById(id);
            var buses = _rutasRepository.GetAll();

            if (rutas == null)
            {
                return NotFound();
            }

            ViewBag.RutasBuses = new SelectList(
                                       buses,
                                       nameof(RutasModel.id_Rutas),
                                       nameof(RutasModel.Nombre_Ruta),
                                       rutas?.id_Rutas
                                       );

            return View(rutas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RutasModel rutas)
        {
            try
            {
                _rutasRepository.Edit(rutas);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(rutas);

            }
        }
    }
}
