using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;
using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniPlantaJugos.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly AppDbContext _context;

        public OrdenesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ordenes (Jefe de planta puede consultar y filtrar)
        public async Task<IActionResult> Index(DateTime? fecha, EstadoOrden? estado, int? productoId)
        {
            var query = _context.OrdenesProd
                .Include(o => o.Producto)
                .Include(o => o.Maquina)
                .AsQueryable();

            if (fecha.HasValue)
            {
                query = query.Where(o => o.Fecha.Date == fecha.Value.Date);
            }

            if (estado.HasValue)
            {
                query = query.Where(o => o.Estado == estado.Value);
            }

            if (productoId.HasValue)
            {
                query = query.Where(o => o.ProductoId == productoId.Value);
            }

            ViewBag.Productos = new SelectList(await _context.Productos.ToListAsync(), "Id", "Nombre", productoId);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoOrden)), estado);

            return View(await query.OrderByDescending(o => o.Fecha).ToListAsync());
        }

        // GET: Ordenes/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos.Where(p => p.Activo), "Id", "Nombre");
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas.Where(m => m.Estado == EstadoMaquina.Operativa), "Id", "Nombre");
            return View();
        }

        // POST: Ordenes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,MaquinaId,CantUnidades,Fecha,Usuario")] OrdenProd ordenProd)
        {
            if (ModelState.IsValid)
            {
                ordenProd.Estado = EstadoOrden.Pendiente;
                _context.Add(ordenProd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos.Where(p => p.Activo), "Id", "Nombre", ordenProd.ProductoId);
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas.Where(m => m.Estado == EstadoMaquina.Operativa), "Id", "Nombre", ordenProd.MaquinaId);
            return View(ordenProd);
        }

        // POST: Ordenes/ChangeState
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeState(int id, EstadoOrden nuevoEstado)
        {
            var orden = await _context.OrdenesProd.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }

            orden.Estado = nuevoEstado;
            _context.Update(orden);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Ordenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenProd = await _context.OrdenesProd
                .Include(o => o.Producto)
                .Include(o => o.Maquina)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (ordenProd == null)
            {
                return NotFound();
            }

            // Buscar si tiene control de calidad asociado
            var controlCalidad = await _context.ControlesCalidad
                .FirstOrDefaultAsync(c => c.OrdenProdId == id);
                
            ViewBag.ControlCalidad = controlCalidad;

            return View(ordenProd);
        }
    }
}
