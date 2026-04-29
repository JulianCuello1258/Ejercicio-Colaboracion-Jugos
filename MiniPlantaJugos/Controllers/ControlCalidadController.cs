using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;
using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MiniPlantaJugos.Controllers
{
    public class ControlCalidadController : Controller
    {
        private readonly AppDbContext _context;

        public ControlCalidadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ControlCalidad (Encargado de calidad revisa problemas graves)
        public async Task<IActionResult> Index()
        {
            // Listar solamente controles rechazados
            var problemasGraves = await _context.ControlesCalidad
                .Include(c => c.ProdRevisada)
                    .ThenInclude(o => o.Producto)
                .Where(c => c.Resultado == ResultadoControl.Rechazado)
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();

            return View(problemasGraves);
        }

        // GET: ControlCalidad/Create
        public IActionResult Create(int? ordenId)
        {
            // Solo permitir hacer control de calidad de órdenes finalizadas que no tengan control previo
            var ordenesDisponibles = _context.OrdenesProd
                .Include(o => o.Producto)
                .Where(o => o.Estado == EstadoOrden.Finalizada && !_context.ControlesCalidad.Any(c => c.OrdenProdId == o.Id))
                .Select(o => new {
                    Id = o.Id,
                    Descripcion = $"Orden #{o.Id} - {o.Producto.Nombre} ({o.CantUnidades} uds)"
                })
                .ToList();

            ViewData["OrdenProdId"] = new SelectList(ordenesDisponibles, "Id", "Descripcion", ordenId);
            return View();
        }

        // POST: ControlCalidad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdenProdId,Resultado,Fecha,Observacion")] ControlCalidad controlCalidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(controlCalidad);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Ordenes", new { id = controlCalidad.OrdenProdId });
            }

            var ordenesDisponibles = _context.OrdenesProd
                .Include(o => o.Producto)
                .Where(o => o.Estado == EstadoOrden.Finalizada)
                .Select(o => new {
                    Id = o.Id,
                    Descripcion = $"Orden #{o.Id} - {o.Producto.Nombre} ({o.CantUnidades} uds)"
                })
                .ToList();

            ViewData["OrdenProdId"] = new SelectList(ordenesDisponibles, "Id", "Descripcion", controlCalidad.OrdenProdId);
            return View(controlCalidad);
        }
    }
}
