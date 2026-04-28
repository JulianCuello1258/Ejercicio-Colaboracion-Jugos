using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;
using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Controllers
{
    public class IncidentesController : Controller
    {
        private readonly AppDbContext _context;

        public IncidentesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Incidentes
        public async Task<IActionResult> Index(EstadoIncidente? filtroEstado, Severidad? filtroSeveridad)
        {
            IQueryable<Incidente> query = _context.Incidentes
                .Include(i => i.Maquina)
                .Include(i => i.Usuario);

            if (filtroEstado.HasValue)
                query = query.Where(i => i.Estado == filtroEstado.Value);
            if (filtroSeveridad.HasValue)
                query = query.Where(i => i.Severidad == filtroSeveridad.Value);

            ViewBag.FiltroEstado = filtroEstado;
            ViewBag.FiltroSeveridad = filtroSeveridad;
            return View(await query.ToListAsync());
        }

        // GET: Incidentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Incidente? incidente = await _context.Incidentes
                .Include(i => i.Maquina)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (incidente == null) return NotFound();
            return View(incidente);
        }

        // GET: Incidentes/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Maquinas = new SelectList(await _context.Maquinas.ToListAsync(), "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nombre");
            return View();
        }

        // POST: Incidentes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Incidente incidente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Maquinas = new SelectList(await _context.Maquinas.ToListAsync(), "Id", "Nombre", incidente.MaquinaId);
            ViewBag.Usuarios = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nombre", incidente.UsuarioId);
            return View(incidente);
        }

        // POST: Incidentes/Resolver/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resolver(int id)
        {
            Incidente? incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null) return NotFound();
            incidente.Estado = EstadoIncidente.Resuelto;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
