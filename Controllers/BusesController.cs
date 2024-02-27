using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using RutasBuses;
using RutasBuses.Models;
using RutasBuses.Repositories;

namespace RutasBuses.Controllers
{
    public class BusesController : Controller
    {
        private readonly IBusesRepositories _busesRepository;

        public BusesController(IBusesRepositories busesRepository)
        {
            _busesRepository = busesRepository;
        }

        public IActionResult Index()
        {
            return View(_busesRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var rutasBuses = _busesRepository.GetAll();

            ViewBag.RutasBuses = new SelectList(rutasBuses,
                                       nameof(RutasModel.id_Rutas),
                                       nameof(RutasModel.Nombre_Ruta));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BusesModel buses)
        {
            try
            {
                _busesRepository.Add(buses);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(buses);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var buses = _busesRepository.GetBusesById(id);
            var rutas = _busesRepository.GetAll();

            if (buses == null)
            {
                return NotFound();
            }

            ViewBag.RutasBuses = new SelectList(
                                       rutas,
                                       nameof(BusesModel.id_Buses),
                                       nameof(BusesModel.Capacidad),
                                       buses?.id_Buses
                                       );

            return View(buses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BusesModel buses)
        {
            try
            {
                _busesRepository.Edit(buses);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(buses);

            }
        }
    }
}
