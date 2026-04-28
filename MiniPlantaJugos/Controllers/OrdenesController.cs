using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;
using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly AppDbContext _context;

        public OrdenesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ordenes
        public async Task<IActionResult> Index(EstadoOrden? filtroEstado, int? filtroProductoId, DateTime? filtroFecha)
        {
            IQueryable<Orden> query = _context.Ordenes
                .Include(o => o.Producto)
                .Include(o => o.Maquina)
                .Include(o => o.Usuario);

            if (filtroEstado.HasValue)
                query = query.Where(o => o.Estado == filtroEstado.Value);
            if (filtroProductoId.HasValue)
                query = query.Where(o => o.ProductoId == filtroProductoId.Value);
            if (filtroFecha.HasValue)
                query = query.Where(o => o.FechaProduccion.Date == filtroFecha.Value.Date);

            ViewBag.FiltroEstado = filtroEstado;
            ViewBag.FiltroProductoId = filtroProductoId;
            ViewBag.FiltroFecha = filtroFecha;
            ViewBag.Productos = new SelectList(await _context.Productos.ToListAsync(), "Id", "Nombre", filtroProductoId);
            return View(await query.OrderByDescending(o => o.FechaProduccion).ToListAsync());
        }

        // GET: Ordenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Orden? orden = await _context.Ordenes
                .Include(o => o.Producto)
                .Include(o => o.Maquina)
                .Include(o => o.Usuario)
                .Include(o => o.ControlesCalidad)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (orden == null) return NotFound();
            return View(orden);
        }

        // GET: Ordenes/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Productos = new SelectList(await _context.Productos.Where(p => p.Activo).ToListAsync(), "Id", "Nombre");
            ViewBag.Maquinas = new SelectList(await _context.Maquinas.Where(m => m.Estado == EstadoMaquina.Operativa).ToListAsync(), "Id", "Nombre");
            ViewBag.Usuarios = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nombre");
            return View();
        }

        // POST: Ordenes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Productos = new SelectList(await _context.Productos.Where(p => p.Activo).ToListAsync(), "Id", "Nombre", orden.ProductoId);
            ViewBag.Maquinas = new SelectList(await _context.Maquinas.Where(m => m.Estado == EstadoMaquina.Operativa).ToListAsync(), "Id", "Nombre", orden.MaquinaId);
            ViewBag.Usuarios = new SelectList(await _context.Usuarios.ToListAsync(), "Id", "Nombre", orden.UsuarioId);
            return View(orden);
        }

        // POST: Ordenes/CambiarEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, EstadoOrden nuevoEstado)
        {
            Orden? orden = await _context.Ordenes.FindAsync(id);
            if (orden == null) return NotFound();
            orden.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
