using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;
using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Controllers
{
    public class ControlCalidadController : Controller
    {
        private readonly AppDbContext _context;

        public ControlCalidadController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ControlCalidad
        public async Task<IActionResult> Index(ResultadoCalidad? filtroResultado)
        {
            IQueryable<ControlCalidad> query = _context.ControlesCalidad
                .Include(c => c.Orden)
                    .ThenInclude(o => o!.Producto);

            if (filtroResultado.HasValue)
                query = query.Where(c => c.Resultado == filtroResultado.Value);

            ViewBag.FiltroResultado = filtroResultado;
            return View(await query.OrderByDescending(c => c.FechaControl).ToListAsync());
        }

        // GET: ControlCalidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            ControlCalidad? control = await _context.ControlesCalidad
                .Include(c => c.Orden)
                    .ThenInclude(o => o!.Producto)
                .Include(c => c.Orden)
                    .ThenInclude(o => o!.Maquina)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (control == null) return NotFound();
            return View(control);
        }

        // GET: ControlCalidad/Create
        public async Task<IActionResult> Create(int? ordenId)
        {
            List<Orden> ordenes = await _context.Ordenes
                .Include(o => o.Producto)
                .ToListAsync();
            ViewBag.Ordenes = new SelectList(ordenes.Select(o => new { o.Id, Display = $"{o.Producto?.Nombre} - {o.CantidadUnidades} uds ({o.FechaProduccion:dd/MM/yyyy})" }), "Id", "Display", ordenId);
            return View();
        }

        // POST: ControlCalidad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ControlCalidad control)
        {
            if (ModelState.IsValid)
            {
                _context.Add(control);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Orden> ordenes = await _context.Ordenes.Include(o => o.Producto).ToListAsync();
            ViewBag.Ordenes = new SelectList(ordenes.Select(o => new { o.Id, Display = $"{o.Producto?.Nombre} - {o.CantidadUnidades} uds ({o.FechaProduccion:dd/MM/yyyy})" }), "Id", "Display", control.OrdenId);
            return View(control);
        }
    }
}
