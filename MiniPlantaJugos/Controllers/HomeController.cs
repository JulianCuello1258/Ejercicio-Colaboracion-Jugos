using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Data;

namespace MiniPlantaJugos.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalProductos = await _context.Productos.CountAsync(p => p.Activo);
            ViewBag.TotalMaquinas = await _context.Maquinas.CountAsync();
            ViewBag.MaquinasOperativas = await _context.Maquinas.CountAsync(m => m.Estado == Enums.EstadoMaquina.Operativa);
            return View();
        }
    }
}
