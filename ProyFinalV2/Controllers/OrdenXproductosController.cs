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
    public class OrdenXproductosController : Controller
    {
        private readonly Ecommerce2Context _context;

        public OrdenXproductosController(Ecommerce2Context context)
        {
            _context = context;
        }

        // GET: OrdenXproductos
        public async Task<IActionResult> Index()
        {
            var ecommerce2Context = _context.OrdenXproductos.Include(o => o.Orden).Include(o => o.Producto).Include(o => o.Orden.IdUsuarioNavigation );
            return View(await ecommerce2Context.ToListAsync());
        }

        // GET: OrdenXproductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenXproductos == null)
            {
                return NotFound();
            }

            var ordenXproducto = await _context.OrdenXproductos
                .Include(o => o.Orden)
                .Include(o => o.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenXproducto == null)
            {
                return NotFound();
            }

            return View(ordenXproducto);
        }

        // GET: OrdenXproductos/Create
        public IActionResult Create()
        {
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: OrdenXproductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrdenId,ProductoId")] OrdenXproducto ordenXproducto)
        {
                _context.Add(ordenXproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id", ordenXproducto.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", ordenXproducto.ProductoId);
            return View(ordenXproducto);
        }

        // GET: OrdenXproductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenXproductos == null)
            {
                return NotFound();
            }

            var ordenXproducto = await _context.OrdenXproductos.FindAsync(id);
            if (ordenXproducto == null)
            {
                return NotFound();
            }
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id", ordenXproducto.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", ordenXproducto.ProductoId);
            return View(ordenXproducto);
        }

        // POST: OrdenXproductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrdenId,ProductoId")] OrdenXproducto ordenXproducto)
        {
            if (id != ordenXproducto.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(ordenXproducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenXproductoExists(ordenXproducto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id", ordenXproducto.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", ordenXproducto.ProductoId);
            return View(ordenXproducto);
        }

        // GET: OrdenXproductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenXproductos == null)
            {
                return NotFound();
            }

            var ordenXproducto = await _context.OrdenXproductos
                .Include(o => o.Orden)
                .Include(o => o.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenXproducto == null)
            {
                return NotFound();
            }

            return View(ordenXproducto);
        }

        // POST: OrdenXproductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenXproductos == null)
            {
                return Problem("Entity set 'Ecommerce2Context.OrdenXproductos'  is null.");
            }
            var ordenXproducto = await _context.OrdenXproductos.FindAsync(id);
            if (ordenXproducto != null)
            {
                _context.OrdenXproductos.Remove(ordenXproducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenXproductoExists(int id)
        {
          return _context.OrdenXproductos.Any(e => e.Id == id);
        }
    }
}
