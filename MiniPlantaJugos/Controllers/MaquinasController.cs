using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;
using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Controllers
{
    public class MaquinasController : Controller
    {
        private readonly AppDbContext _context;

        public MaquinasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Maquinas
        public async Task<IActionResult> Index(EstadoMaquina? filtroEstado)
        {
            IQueryable<Maquina> query = _context.Maquinas;
            if (filtroEstado.HasValue)
            {
                query = query.Where(m => m.Estado == filtroEstado.Value);
            }
            ViewBag.FiltroEstado = filtroEstado;
            return View(await query.ToListAsync());
        }

        // GET: Maquinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Maquina? maquina = await _context.Maquinas
                .Include(m => m.Usuarios)
                .Include(m => m.Incidentes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maquina == null) return NotFound();
            return View(maquina);
        }

        // GET: Maquinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maquinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maquina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maquina);
        }

        // POST: Maquinas/CambiarEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, EstadoMaquina nuevoEstado)
        {
            Maquina? maquina = await _context.Maquinas.FindAsync(id);
            if (maquina == null) return NotFound();
            maquina.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
