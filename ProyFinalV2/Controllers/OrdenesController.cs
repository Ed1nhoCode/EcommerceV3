using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyFinalV2.Models;

namespace ProyFinalV2.Controllers
{
    [Authorize]
    public class OrdenesController : Controller
    {
        private readonly Ecommerce2Context _context;

        public OrdenesController(Ecommerce2Context context)
        {
            _context = context;
        }

        // GET: Ordenes
        public async Task<IActionResult> Index()
        {
            var ecommerce2Context = _context.Ordenes.Include(o => o.IdUsuarioNavigation);
            return View(await ecommerce2Context.ToListAsync());
        }

        // GET: Ordenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var ordene = await _context.Ordenes
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordene == null)
            {
                return NotFound();
            }

            return View(ordene);
        }

        // GET: Ordenes/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Ordenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,DireccionEnvio,FechaOrden,StatusOrden,Total")] Ordene ordene)
        {

                _context.Add(ordene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", ordene.IdUsuario);
            return View(ordene);
        }

        // GET: Ordenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var ordene = await _context.Ordenes.FindAsync(id);
            if (ordene == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", ordene.IdUsuario);
            return View(ordene);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,DireccionEnvio,FechaOrden,StatusOrden,Total")] Ordene ordene)
        {
            if (id != ordene.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(ordene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdeneExists(ordene.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", ordene.IdUsuario);
            return View(ordene);
        }

        // GET: Ordenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var ordene = await _context.Ordenes
                .Include(o => o.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordene == null)
            {
                return NotFound();
            }

            return View(ordene);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordenes == null)
            {
                return Problem("Entity set 'Ecommerce2Context.Ordenes'  is null.");
            }
            var ordene = await _context.Ordenes.FindAsync(id);
            if (ordene != null)
            {
                _context.Ordenes.Remove(ordene);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdeneExists(int id)
        {
          return _context.Ordenes.Any(e => e.Id == id);
        }
    }
}
